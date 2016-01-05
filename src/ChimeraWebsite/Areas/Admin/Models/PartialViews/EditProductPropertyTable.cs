using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChimeraWebsite.Areas.Admin.Models.PartialViews
{
    public class EditProductPropertyTable
    {
        public string HeaderText { get; set; }

        public string DescriptionText { get; set; }

        public string KnockoutListName { get; set; }

        public string JSGlobalVariableStaticPropertyKey { get; set; }

        public EditProductPropertyTable()
        {
            HeaderText = string.Empty;
            DescriptionText = string.Empty;
            KnockoutListName = string.Empty;
            JSGlobalVariableStaticPropertyKey = string.Empty;
        }

        public EditProductPropertyTable(string headerText, string descriptionText, string knockoutListName, string jsGlobalVariableStaticPropertyKey)
        {
            HeaderText = headerText;
            DescriptionText = descriptionText;
            KnockoutListName = knockoutListName;
            JSGlobalVariableStaticPropertyKey = jsGlobalVariableStaticPropertyKey;
        }
    }
}