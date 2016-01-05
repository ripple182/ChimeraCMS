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
    public class AdminUserProcessor : IProcessor
    {
        public string FilePath { get; set; }

        public AdminUserProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public void ProcessFile()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(FilePath);



            foreach (var Node in XmlDoc.DocumentElement.GetElementsByTagName("AdminUser"))
            {
                XmlElement Element = (XmlElement) Node;

                AdminUser AdmUser = new AdminUser();

                AdmUser.Username = Element.GetElementsByTagName("Username")[0].InnerText;
                AdmUser.Email = Element.GetElementsByTagName("Email")[0].InnerText;
                AdmUser.Hashed_Password = Element.GetElementsByTagName("Hashed_Password")[0].InnerText;
                AdmUser.Active = true;

                foreach (var ChildNode in Element.GetElementsByTagName("Role"))
                {
                    XmlElement ChildElement = (XmlElement)ChildNode;

                    AdmUser.RoleList.Add(ChildElement.InnerText);
                }

                AdminUserDAO.Save(AdmUser);
            }
        }
    }
}
