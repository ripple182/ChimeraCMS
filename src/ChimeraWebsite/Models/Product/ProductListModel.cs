using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CE = Chimera.Entities.Product;

namespace ChimeraWebsite.Models.Product
{
    public class ProductListModel
    {
        public string ViewType { get; set; }

        public List<CE.Product> ProductList { get; set; }

        public ProductListModel(string viewType, List<CE.Product> productList)
        {
            ViewType = viewType;
            ProductList = productList;
        }
    }
}