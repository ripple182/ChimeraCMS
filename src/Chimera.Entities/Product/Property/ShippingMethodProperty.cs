using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Product.Property
{
    public class ShippingMethodProperty
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ShippingMethodProperty()
        {
            Name = string.Empty;
            Price = 0;
        }

        public ShippingMethodProperty(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
