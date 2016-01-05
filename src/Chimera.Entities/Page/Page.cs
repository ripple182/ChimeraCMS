using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Chimera.Entities.Page.Module;

namespace Chimera.Entities.Page
{
    public class Page
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid PageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDateUTC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PageFriendlyURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RowModule> RowModuleList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Page()
        {
            Id = string.Empty;
            PageId = Guid.Empty;
            ModifiedDateUTC = DateTime.MinValue;
            PageTitle = string.Empty;
            PageFriendlyURL = string.Empty;
            Published = false;
            RowModuleList = new List<Module.RowModule>();
        }

        public void CreateDefaultNewPage()
        {
            PageId = Guid.NewGuid();

            /*RowModule RowModule = new RowModule();

            ColumnModule ColumnModule = new ColumnModule();

            ColumnModule.ModuleTypeName = "TitleDescription";

            ColumnModule.ChildrenValueDictionary.Add("title", new ColumnModuleChild("Page Title"));
            ColumnModule.ChildrenValueDictionary.Add("description", new ColumnModuleChild("Praesent at tellus porttitor nisl porttitor sagittis. Mauris in massa ligula, a tempor nulla. Ut tempus interdum mauris vel vehicula. Nulla ullamcorper tortor commodo in sagittis est accumsan."));

            RowModule.ColumnModuleList.Add(ColumnModule);

            RowModuleList.Add(RowModule);*/
        }
    }
}
