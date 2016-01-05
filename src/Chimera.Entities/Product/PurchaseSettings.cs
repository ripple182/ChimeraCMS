using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Product.Property;

namespace Chimera.Entities.Product
{
    public class PurchaseSettings
    {
        public decimal PurchasePrice { get; set; }

        public int StockLevel { get; set; }

        public List<ShippingMethodProperty> ShippingMethodList { get; set; }

        public PurchaseSettings()
        {
            PurchasePrice = 0;
            StockLevel = 0;
            ShippingMethodList = new List<ShippingMethodProperty>();
        }
    }
}
