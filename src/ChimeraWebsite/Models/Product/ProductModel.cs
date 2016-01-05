using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CE = Chimera.Entities.Product;

namespace ChimeraWebsite.Models.Product
{
    public class ProductModel
    {
        public string ViewType { get; set; }

        public CE.Product Product { get; set; }

        public ProductModel(string viewType, CE.Product product)
        {
            ViewType = viewType;
            Product = product;
        }
    }
}