using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class PurchaseOrderRoles
    {
        public const string VIEW = "purchase-order-view";

        public const string EDIT = "purchase-order-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT;
            }
        }
    }
}
