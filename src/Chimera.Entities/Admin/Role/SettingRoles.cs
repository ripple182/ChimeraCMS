using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Admin.Role
{
    public static class SettingRoles
    {
        public const string VIEW = "setting-view";

        public const string EDIT_SCHEMA = "setting-schema-edit";

        public const string EDIT_VALUES = "setting-value-edit";

        public static string ALL
        {
            get
            {
                return VIEW + ";" + EDIT_SCHEMA;
            }
        }
    }
}
