using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace AppClientSuperSocket
{
    public class XmlHelper<T> where T : class, new()
    {
        public static T Load(string path, string fieldName = "")
        {
            var doc = new XmlDocument();
            var info = new T();

            doc.Load(path);

            if (string.IsNullOrEmpty(fieldName))
                GetValue(doc.DocumentElement, ref info);
            else
                GetValue(doc.DocumentElement, ref info, fieldName);

            return info;
        }

        public static T LoadXml(string content)
        {
            var doc = new XmlDocument();
            var info = new T();

            doc.LoadXml(content);
            GetValue(doc.DocumentElement, ref info);

            return info;
        }

        private static void GetValue(XmlNode node, ref T info)
        {
            if (node.HasChildNodes == true && node.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    if (subNode.NodeType != XmlNodeType.Comment)
                        GetValue(subNode, ref info);
                }
            }
            else
            {
                PropertyInfo[] pInfo = typeof(T).GetProperties();

                foreach (PropertyInfo p in pInfo)
                {
                    if (p.Name.ToLower() == node.ParentNode.Name.ToLower())
                    {
                        p.SetValue(info, node.Value, null);
                    }
                }
            }
        }

        private static void GetValue(XmlNode node, ref T info, string fieldName)
        {
            if (node.HasChildNodes == true && node.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    if (subNode.NodeType != XmlNodeType.Comment && fieldName == subNode.Name)
                    {
                        PropertyInfo[] pInfo = typeof(T).GetProperties();

                        foreach (PropertyInfo p in pInfo)
                        {
                            if (p.Name.ToLower() == node.ParentNode.Name.ToLower())
                            {
                                p.SetValue(info, node.Value, null);
                            }
                        }
                    }
                }
            }
        }

        private static void SetValue(XmlNode node, string itemName, string value, ref bool update)
        {
            if (node.HasChildNodes == true && node.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    SetValue(subNode, itemName, value, ref update);
                }
            }
            else
            {
                string name = node.ParentNode.Name.ToLower();
                if (name == itemName.ToLower())
                {
                    if (!string.Equals(node.Value, value))
                    {
                        node.Value = value;
                        update = true;
                    }
                }
            }
        }

        public static void UpdateXmlElement(string path, string itemName, string value)
        {
            var doc = new XmlDocument();
            var updated = false;

            doc.Load(path);
            SetValue(doc.DocumentElement, itemName, value, ref updated);

            if (updated) doc.Save(path);
        }

        public static void CreateXmlElement(string path, string fieldName, string itemName, string value = "")
        {
            var doc = new XmlDocument();
            doc.Load(path);

            if (doc.DocumentElement.HasChildNodes == true && doc.DocumentElement.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in doc.DocumentElement.ChildNodes)
                {
                    if (subNode.Name == fieldName)
                    {
                        foreach (XmlNode node in subNode)
                        {
                            if (node.Name == itemName)
                            {
                                // if values are equal, don't update xml file 
                                if (!string.Equals(node.InnerText, value))
                                {
                                    node.InnerText = value;
                                    doc.Save(path);
                                }
                                return;
                            }
                        }

                        XmlElement element = doc.CreateElement(itemName);
                        element.InnerText = value;
                        subNode.AppendChild(element);
                        doc.Save(path);
                    }
                }
            }
        }

        public static void UpdateFieldName(string path, string fieldName, string itemName, string newItemName)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            if (doc.DocumentElement.HasChildNodes == true && doc.DocumentElement.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in doc.DocumentElement.ChildNodes)
                {
                    if (subNode.Name == fieldName)
                    {
                        foreach (XmlNode node in subNode)
                        {
                            if (node.Name == itemName)
                            {
                                string value = node.InnerText;

                                XmlElement element = doc.CreateElement(newItemName);
                                element.InnerText = value;

                                subNode.ReplaceChild(element, node);
                                doc.Save(path);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveXmlElement(string path, string fieldName, string itemName)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            if (doc.DocumentElement.HasChildNodes == true && doc.DocumentElement.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in doc.DocumentElement.ChildNodes)
                {
                    if (subNode.Name == fieldName)
                    {
                        foreach (XmlNode node in subNode)
                        {
                            if (node.Name == itemName)
                            {
                                subNode.RemoveChild(node);
                                doc.Save(path);
                                return;
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveXmlElement(string path, string fieldName)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            if (doc.DocumentElement.HasChildNodes == true && doc.DocumentElement.NodeType != XmlNodeType.Comment)
            {
                foreach (XmlNode subNode in doc.DocumentElement.ChildNodes)
                {
                    if (subNode.Name == fieldName)
                    {
                        subNode.RemoveAll();

                        if (subNode.ParentNode != null)
                            subNode.ParentNode.RemoveChild(subNode);

                        doc.Save(path);
                    }
                }
            }
        }
    }
}
