using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities;

namespace Chimera.Entities.Settings
{
    public class SettingAttribute
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public SettingAttribute()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
    }
}
