using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chimera.Entities.Admin;
using Chimera.Entities.Page.Module;
using Chimera.Entities.Product;
using Chimera.Entities.Property;
using Chimera.Entities.Settings;
using Chimera.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using CM = System.Configuration.ConfigurationManager;
using System.Xml;
using System.IO;
using System.Reflection;
using ChimeraDatabaseInitialize.Processors;
using Chimera.Entities.Dashboard;
using Chimera.Entities.Admin.Role;

namespace ChimeraDatabaseInitialize
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProcessXMLFiles();
            //AddDefaultIndexPage();
        }

        private static void ProcessXMLFiles()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            Path.GetDirectoryName(path);

            string AssemblyDirectory = Path.GetDirectoryName(path);

            ProcessFiles(AssemblyDirectory + "/Data/Common");
            ProcessFiles(AssemblyDirectory + "/Data/Templates/" + CM.AppSettings["TemplateName"]);
            ProcessFiles(AssemblyDirectory + "/Data/Customers/" + CM.AppSettings["CustomerName"]);
        }

        private static void Ntofication()
        {
            Notification NewNotification = new Notification();

            NewNotification.EntityId = "";
            NewNotification.MvcUrl.Action = "Edit";
            NewNotification.MvcUrl.Controller = "PurchaseOrders";
            NewNotification.Description = "New Notification";
            NewNotification.NotificationType = NotificationType.NEW_PURCHASE_ORDER;
            NewNotification.WarningLevel = WarningLevelType.NEUTRAL;
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.VIEW);
            NewNotification.ViewAdminUserRolesRequired.Add(PurchaseOrderRoles.EDIT);

            NewNotification.ActionList.Insert(0, new NotificationAction("Edit", "glyphicon glyphicon-pencil", "Edit", "PurchaseOrders"));

            DashboardNotificationDAO.Save(NewNotification);
        }

        /// <summary>
        /// Processor a XML file into the MongoDB.
        /// </summary>
        /// <param name="directoryPath">the directory of all the files to process</param>
        private static void ProcessFiles(string directoryPath)
        {
            foreach (var FileName in Directory.GetFiles(directoryPath))
            {
                IProcessor Processor = IProcessorFactory.GetProcessor(FileName);

                if (Processor != null)
                {
                    Processor.ProcessFile();
                }
            }
        }

        private static void AddDefaultIndexPage()
        {
            Chimera.Entities.Page.Page Page = new Chimera.Entities.Page.Page();

            Page.PageId = new Guid("84bc9530-e26d-4efe-bc9c-8d3d71a09053");
            Page.PageTitle = "Index Page";
            Page.PageFriendlyURL = "Index";
            Page.Published = true;

            for (int i = 1; i < 13; i++)
            {
                RowModule RowModule = new RowModule();

                RowModule.AdditionalClassNames = "visible-xs visible-sm visible-md visible-lg";

                ColumnModule ColumnModule = new ColumnModule();

                ColumnModule.ModuleTypeName = "_Common_AlertSuccess";

                ColumnModule.ChildrenValueDictionary.Add("Text", new ColumnModuleChild("Praesent at tellus porttitor nisl porttitor sagittis. Mauris in massa ligula, a tempor nulla. Ut tempus interdum mauris vel vehicula. Nulla ullamcorper tortor commodo in sagittis est accumsan."));
                ColumnModule.AdditionalClassNames = "col-lg-12 col-md-12 col-sm-12 col-xs-12 visible-xs visible-sm visible-md visible-lg";

                RowModule.ColumnModuleList.Add(ColumnModule);

                Page.RowModuleList.Add(RowModule);
            }

            Chimera.DataAccess.PageDAO.Save(Page);
        }
    }
}
