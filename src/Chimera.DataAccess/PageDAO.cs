using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Page;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class PageDAO
    {
        private const string COLLECTION_NAME = "Pages";

        /// <summary>
        /// Will save a brand new page revision so we can maintain the old page structure for revision history.
        /// Method will generate a new MongoDB ObjectId and UTC datetime for the entity.
        /// </summary>
        /// <param name="page">page to save</param>
        /// <returns>bool</returns>
        public static bool Save(Page page)
        {
            bool SaveSuccessful = true;

            //Create a new object id so we can maintain the revision history of a page, the "PageId" attribute will remain the same.
            page.Id = ObjectId.GenerateNewId().ToString();

            //Timestamp of when the page was saved.
            page.ModifiedDateUTC = DateTime.UtcNow;

            //if we are publishing a page then make sure all pages in DB that exist with same page id are set to not published.
            if (page.Published)
            {
                //set all records with same page id to false before saving new page.

                var UpdateQuery = Query<Page>.EQ(e => e.PageId, page.PageId);
                var UpdateSetStatement = Update<Page>.Set(e => e.Published, false);

                SaveSuccessful = Execute.Update<Page>(COLLECTION_NAME, UpdateQuery, UpdateSetStatement);
            }

            SaveSuccessful = Execute.Save<Page>(COLLECTION_NAME, page);

            //delete versions more than 10
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            List<Page> PageList = (from e in Collection.AsQueryable<Page>() where e.PageId.Equals(page.PageId) orderby e.ModifiedDateUTC descending select e).Skip(10).ToList();

            List<string> PageIdList = (List<string>) (from e in PageList select e.Id).ToList();

            var DeleteQuery = Query<Page>.In(e => e.Id, PageIdList);

            return SaveSuccessful && Execute.Delete<Page>(COLLECTION_NAME, DeleteQuery);
        }

        /// <summary>
        /// Load the only published version of a page.
        /// </summary>
        /// <param name="pageURL">the unique page friendly url (i.e. "Blog", "BuyShirts", etc).</param>
        /// <returns>A single page object.</returns>
        public static Page LoadByURL(string pageURL)
        {
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Page>() where e.PageFriendlyURL.ToUpper() == pageURL.ToUpper() && e.Published select e).FirstOrDefault();
        }

        /// <summary>
        /// Simply load a page by its unique Mongo Bson Id.
        /// </summary>
        /// <param name="bsonId">The unqiue Mongo Bson Id.</param>
        /// <returns>A single page object.</returns>
        public static Page LoadByBsonId(string bsonId)
        {
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Page>() where e.Id == bsonId select e).FirstOrDefault();
        }

        /// <summary>
        /// Load the most recent page revision of a specific page type by the page id if it is published.
        /// </summary>
        /// <param name="pageId">PageId that ties page revisions together.</param>
        /// <returns>A single page object.</returns>
        public static Page LoadByPageId(Guid pageId)
        {
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            return (from e in Collection.AsQueryable<Page>() where e.PageId.Equals(pageId) orderby e.Published descending, e.ModifiedDateUTC descending select e).FirstOrDefault();
        }

        /// <summary>
        /// Load a list of existing page types.
        /// </summary>
        /// <returns>A list of PageType objects.</returns>
        public static List<PageType> LoadPageTypes()
        {
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            List<Page> PageList = (from e in Collection.AsQueryable<Page>() orderby e.PageId, e.ModifiedDateUTC descending select e).ToList();

            List<PageType> NewPageTypeList = new List<PageType>();

            List<Guid> ProcessedPageIds = new List<Guid>();

            if (PageList != null && PageList.Count > 0)
            {
                foreach (var Page in PageList)
                {
                    if (!ProcessedPageIds.Contains(Page.PageId))
                    {
                        NewPageTypeList.Add(new PageType { LatestVersionPublished = Page.Published, PageId = Page.PageId, PageTitle = Page.PageTitle, PageFriendlyURL = Page.PageFriendlyURL, ModifiedDateUTC = Page.ModifiedDateUTC, Published = Page.Published });
                        ProcessedPageIds.Add(Page.PageId);
                    }
                    else if (Page.Published)
                    {
                        PageType PageType = NewPageTypeList.Where(e => e.PageId.Equals(Page.PageId)).FirstOrDefault();

                        int Index = NewPageTypeList.IndexOf(PageType);

                        if (Index != -1 && PageType != null)
                        {
                            NewPageTypeList[Index].Published = true;
                        }
                    }
                }

                return NewPageTypeList;
            }

            return null;
        }

        /// <summary>
        /// Load a list of PageRevisions for a specific page id, will be ordered by descending date (most recent first).
        /// </summary>
        /// <param name="PageId">The page id that stays the same between each revision.</param>
        /// <returns>Lite list of page revisions for display.</returns>
        public static List<PageRevision> LoadRevisionHistory(Guid PageId)
        {
            MongoCollection<Page> Collection = Execute.GetCollection<Page>(COLLECTION_NAME);

            List<PageRevision> NewPageRevisionList = new List<PageRevision>();

            List<Page> PageList = (from e in Collection.AsQueryable<Page>() where e.PageId.Equals(PageId) orderby e.ModifiedDateUTC descending select e).ToList();

            if (PageList != null && PageList.Count > 0)
            {
                foreach (var Page in PageList)
                {
                    NewPageRevisionList.Add(new PageRevision { Id = Page.Id, PageTitle = Page.PageTitle, PageFriendlyURL = Page.PageFriendlyURL, Published = Page.Published, ModifiedDateUTC = Page.ModifiedDateUTC  });
                }

            }

            return NewPageRevisionList;
        }
    }
}
