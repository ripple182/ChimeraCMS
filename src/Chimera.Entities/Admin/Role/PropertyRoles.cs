using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class PropertyRoles
    {
        public const string VIEW = "property-view";

        public const string EDIT = "property-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT;
            }
        }
    }
}
