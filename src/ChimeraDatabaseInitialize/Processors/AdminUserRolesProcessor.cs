using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Chimera.Entities.Admin;
using Chimera.DataAccess;

namespace ChimeraDatabaseInitialize.Processors
{
    public class AdminUserRolesProcessor : IProcessor
    {
        public string FilePath { get; set; }

        public AdminUserRolesProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public void ProcessFile()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(FilePath);

            foreach (var Node in XmlDoc.DocumentElement.GetElementsByTagName("AdminUserRole"))
            {
                XmlElement Element = (XmlElement)Node;

                AdminUserRole AdmUserRole = new AdminUserRole();

                AdmUserRole.Name = Element.GetElementsByTagName("Name")[0].InnerText;
                AdmUserRole.Description = Element.GetElementsByTagName("Description")[0].InnerText;

                AdminUserRoleDAO.Save(AdmUserRole);
            }
        }
    }
}
