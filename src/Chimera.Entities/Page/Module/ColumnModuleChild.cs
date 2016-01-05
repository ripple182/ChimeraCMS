using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Page.Module
{
    public class ColumnModuleChild
    {
        public string Value { get; set; }

        public bool Active { get; set; }

        public ColumnModuleChild(string value = "")
        {
            Value = value;
            Active = true;
        }
    }
}
