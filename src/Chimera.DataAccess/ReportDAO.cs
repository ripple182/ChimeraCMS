using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Report;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using CompanyCommons.DataAccess.MongoDB;

namespace Chimera.DataAccess
{
    public static class ReportDAO
    {
        private const string COLLECTION_NAME = "PageViews";

        public static List<string> LoadUniquePageTypes()
        {
            MongoCollectionBase<PageView> Collection = Execute.GetCollection<PageView>(COLLECTION_NAME);

            //grab the last viewed page and set the exit time if exists
            IQueryable<string> PageTypes = Collection.AsQueryable<PageView>().Select(e => e.PageFriendlyURL).Distinct();

            return PageTypes != null ?PageTypes.ToList() : new List<string>();
        }

        /// <summary>
        /// Load a list of page views
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="pageFriendlyUrl"></param>
        /// <returns></returns>
        public static List<PageView> LoadPageViews(DateTime dateFrom, DateTime dateTo, string pageFriendlyUrl = "")
        {
            MongoCollection<PageView> Collection = Execute.GetCollection<PageView>(COLLECTION_NAME);

            //grab the last viewed page and set the exit time if exists
            return (from e in Collection.AsQueryable<PageView>() where (pageFriendlyUrl == "" || e.PageFriendlyURL == pageFriendlyUrl) &&  e.PageOpenedDateUTC >= dateFrom && e.PageOpenedDateUTC <= dateTo orderby e.PageOpenedDateUTC select e).ToList();
        }

        /// <summary>
        /// Record a page view
        /// </summary>
        /// <param name="pageView"></param>
        /// <returns></returns>
        public static bool SaveNew(PageView pageView, bool allowPageReportRecording, UserSessionInformation userInfo)
        {
            //if reportings in enabled, and the user is not a bot, and it's been at least 2 seconds since their last page view
            if (allowPageReportRecording && !userInfo.IsBot && (DateTime.UtcNow - userInfo.LastDatePageRecordedUTC).TotalSeconds >= 2)
            {
                bool SaveSuccessful = false;

                SaveSuccessful = UpdateLastPageUserWasOn(allowPageReportRecording, userInfo);

                if (pageView.PageOpenedDateUTC.Equals(DateTime.MinValue))
                {
                    pageView.PageOpenedDateUTC = DateTime.UtcNow;
                }

                //record the current page view
                return Execute.Save<PageView>(COLLECTION_NAME, pageView);
            }

            return true;
        }

        /// <summary>
        /// Grab the last page the user visted if exists and update the exit time
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static bool UpdateLastPageUserWasOn(bool allowPageReportRecording, UserSessionInformation userInfo)
        {
            if (allowPageReportRecording && !userInfo.IsBot)
            {
                MongoCollection<PageView> Collection = Execute.GetCollection<PageView>(COLLECTION_NAME);

                //grab the last viewed page and set the exit time if exists
                PageView LastPageView = (from e in Collection.AsQueryable<PageView>() where e.SessionId == userInfo.SessionId orderby e.PageOpenedDateUTC descending select e).FirstOrDefault();


                if (LastPageView != null && LastPageView.SessionId.Equals(userInfo.SessionId))
                {
                    //only update if was not set before, the exit time will already be set whenever the tab was closed and the session ends 20 mins later
                    if (LastPageView.PageExitDateUTC.Equals(DateTime.MinValue))
                    {
                        LastPageView.PageExitDateUTC = DateTime.UtcNow;

                        return Execute.Save<PageView>(COLLECTION_NAME, LastPageView);
                    }
                }
            }

            return true;
        }
    }
}
