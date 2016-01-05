using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class ProductRoles
    {
        public const string VIEW = "product-view";

        public const string EDIT = "product-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT;
            }
        }
    }
}
