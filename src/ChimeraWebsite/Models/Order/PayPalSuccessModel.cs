using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chimera.Entities.Orders;

namespace ChimeraWebsite.Models.Order
{
    public class PayPalSuccessModel
    {
        public string ViewType { get; set; }

        public PurchaseOrderDetails PayPalPurchaseOrder { get; set; }

        public PayPalSuccessModel(string viewType, PurchaseOrderDetails paypalPurchaseOrder)
        {
            ViewType = viewType;
            PayPalPurchaseOrder = paypalPurchaseOrder;
        }
    }
}