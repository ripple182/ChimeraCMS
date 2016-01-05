using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities.Utility
{
     public class CheckoutPropSettingDuplicant
    {
         public int NumberProcessed { get; set; }

         public int LastProcessedArrayIndex { get; set; }

         public CheckoutPropSettingDuplicant()
         {
             NumberProcessed = 0;
             LastProcessedArrayIndex = 0;
         }

         public CheckoutPropSettingDuplicant(int numberProcessed, int lastProcessedArrayIndex)
         {
             NumberProcessed = numberProcessed;
             LastProcessedArrayIndex = lastProcessedArrayIndex;
         }
    }
}
