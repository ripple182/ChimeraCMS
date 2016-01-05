using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Chimera.Entities.Property
{
    public class Property
    {
        /// <summary>
        /// "COLOR"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// "BLUE", "RED", if product is blue and red.
        /// </summary>
        public HashSet<PropertyValue> Values { get; set; }

        public Property()
        {
            Name = string.Empty;
            Values = new HashSet<PropertyValue>();
        }

        public Property(string name)
        {
            Name = name;
            Values = new HashSet<PropertyValue>();
        }
    }
}
