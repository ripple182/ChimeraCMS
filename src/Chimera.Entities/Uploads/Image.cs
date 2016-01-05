using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Uploads
{
    public class Image
    {
        public ObjectId Id { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public DateTime ModifiedDateUTC { get; set; }

        public string Url { get; set; }

        public Image()
        {
            Id = ObjectId.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
            ModifiedDateUTC = DateTime.MinValue;
        }
    }
}
