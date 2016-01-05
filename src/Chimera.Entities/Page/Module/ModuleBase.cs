using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Page.Module
{
    public abstract class ModuleBase
    {
        /// <summary>
        /// This should handle the column width based on device width, and handle toggling display none depending on device width.
        /// </summary>
        public string AdditionalClassNames { get; set; } 
    }
}
