using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Property
{
    public class PropertyValue
    {
        /// <summary>
        /// "COLOR"
        /// </summary>
        public string Value { get; set; }

        public PropertyValue()
        {
            Value = string.Empty;
        }
    }
}
