using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chimera.Entities
{
    public enum DataEntryType { SmallText = 1, DropdownSelect = 2, MultipleCheckboxes = 3, ImageUpload = 4, MoneyInput = 5, PercentageDecimalInput = 6, SelectMultipleAdminUser = 7 };

    public static class DataEntryTypeProperty
    {
        /// <summary>
        /// List of data entry types that require a static property to be selected
        /// </summary>
        public static List<DataEntryType> DataTypesRequireProperties = new List<DataEntryType>();

        static DataEntryTypeProperty()
        {
            DataTypesRequireProperties.Add(DataEntryType.DropdownSelect);
            DataTypesRequireProperties.Add(DataEntryType.MultipleCheckboxes);
        }
    }
}
