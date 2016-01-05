using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Product
{
    public class ProductImage
    {
        public string ImagePath { get; set; }

        public ProductImage()
        {
            ImagePath = string.Empty;
        }
    }
}
