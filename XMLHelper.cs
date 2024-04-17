using System;

using System.Text;

using System.Xml;

namespace VII
{ 
 class XMLHelper

{

    public static string ParseXML(string xmlText, string nodeName)

    {

        StringBuilder xmlData = new StringBuilder();

        int level = 1;

        //Here we load the file into memory

        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.LoadXml(xmlText);

        //Here we get the document root nodeXmlNode xmlNode=

        XmlNode xmlNode = xmlDocument.DocumentElement;

        XmlNodeList mainNodeList = xmlDocument.GetElementsByTagName(nodeName);

        //Here we speciy padding; th enumber of spaces based on the level in the XML tree

        string pad = new string(' ', level * 2);

        int counter = 0;

        foreach (XmlNode mainNode in mainNodeList)

        {

            counter++;

            //Here we extract possible attributes

            if (mainNode.NodeType == XmlNodeType.Element)

            {

                xmlData.Append(counter + ". " + nodeName + " :" + Environment.NewLine);

                XmlNamedNodeMap mapAttributes = mainNode.Attributes;

                for (int i = 0; i < mapAttributes.Count; i++)

                {

                    xmlData.Append(pad + mapAttributes.Item(i).Name + "=" + mapAttributes.Item(i).Value + Environment.NewLine);

                }

                XmlNodeList childList = mainNode.ChildNodes;

                foreach (XmlNode childNode in childList)

                {

                    xmlData.Append(pad + childNode.Name + "=" + childNode.InnerText + Environment.NewLine);

                }

            }

        }

        return xmlData.ToString();

    }

}

}
