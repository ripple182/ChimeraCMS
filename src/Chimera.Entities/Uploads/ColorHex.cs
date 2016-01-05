using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Uploads
{
    public class ColorHex
    {
        public ObjectId Id { get; set; }

        public string HexValue { get; set; }

        public DateTime ModifiedDateUTC { get; set; }

        public ColorHex(string hexValue)
        {
            Id = ObjectId.GenerateNewId();
            HexValue = hexValue;
            ModifiedDateUTC = DateTime.UtcNow;
        }

        public ColorHex()
        {
            Id = ObjectId.Empty;
            HexValue = string.Empty;
            ModifiedDateUTC = DateTime.MinValue;
        }
    }
}
