using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Chimera.Entities;
using Chimera.Entities.Property;
using Chimera.DataAccess;

namespace ChimeraDatabaseInitialize.Processors
{
    public class StaticPropertyProcessor : IProcessor
    {
        public string FilePath { get; set; }

        public StaticPropertyProcessor(string filePath)
        {
            FilePath = filePath;
        }

        public void ProcessFile()
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.Load(FilePath);

            foreach (var Node in XmlDoc.DocumentElement.GetElementsByTagName("StaticProperty"))
            {
                XmlElement Element = (XmlElement)Node;

                StaticProperty StaticProp = new StaticProperty();

                StaticProp.KeyName = Element.GetElementsByTagName("KeyName")[0].InnerText;
                
                foreach (var ChildNode in Element.GetElementsByTagName("Value"))
                {
                    XmlElement ChildElement = (XmlElement)ChildNode;

                    StaticProp.PropertyNameValues.Add(ChildElement.InnerText);
                }

                StaticPropertyDAO.Save(StaticProp);
            }
        }
    }
}