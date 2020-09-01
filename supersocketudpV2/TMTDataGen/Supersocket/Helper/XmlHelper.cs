using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
namespace CounterTMT
{
    public  class XmlHelper
    {
        private XmlDocument xmlDoc = new XmlDocument();
        public XmlHelper()
        {
        }
        private string xmlFileName = string.Empty;
        /// <summary>
        /// 从文件中读取xml文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public XmlHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                xmlFileName = fileName;
                xmlDoc.Load(fileName);
            }
        }
        /// <summary>
        /// 从资源文件中获取xml文件
        /// </summary>
        /// <param name="assembly">xml所在assembly</param>
        /// <param name="fileName">资源名称</param>
        public XmlHelper(Assembly assembly, string resourceName)
        {
            //LoadXmlString(Helper.GetConfigureResouceFileContent(assembly, resourceName));
        }
        /// <summary>
        /// 直接加载xml字符串
        /// </summary>
        /// <param name="xmlContent">xml字符串</param>
        public void LoadXmlString(string xmlContent)
        {
            if (!string.IsNullOrEmpty(xmlContent))
            {
                xmlDoc.LoadXml(xmlContent);
            }
        }
        #region Read configure node value
        /// <summary>
        /// 读取具体节点的值
        /// </summary>
        /// <param name="sNodeName">所要读取的节点</param>
        /// <returns>节点的值</returns>
        public string GetNodeValue(string sNodeName)
        {
            string strXmlData = string.Empty;
            try
            {
                strXmlData =  xmlDoc.SelectSingleNode(sNodeName).InnerText;
            }
            catch (Exception ex)
            {
                strXmlData = "";
            }
            return strXmlData;
        }
        /// <summary>
        /// 读取具体节点的值
        /// </summary>
        /// <param name="sID">ID属性</param>
        /// <param name="sNodeName">所要读取的节点</param>
        /// <returns>节点的值</returns>
        public string GetIdNodeValue(string sID, string sNodeName)
        {
            return xmlDoc.SelectSingleNode("//root/" + sNodeName + "[@ID='" + sID + "']").InnerText;
        }
        #endregion
        #region Set Node Value
        /// <summary>
        /// 设置节点的值
        /// </summary>
        /// <param name="sNodeName">要设置的节点</param>
        /// <param name="sNodeValue">设置节点的值</param>
        /// <returns>是否成功</returns>
        public void SetNodeValue(string sNodeName, string sNodeValue)
        {
            if (!string.IsNullOrEmpty(xmlFileName))
            {
                xmlDoc.SelectSingleNode("//root/" + sNodeName).InnerText = sNodeValue;
                xmlDoc.Save(xmlFileName);
            }
        }
        #endregion
        /// <summary>
        /// 检查Node是否存在
        /// </summary>
        /// <param name="sNodeName"></param>
        /// <returns></returns>
        public bool HasNode(string sNodeName)
        {
            return xmlDoc.SelectNodes("//root/" + sNodeName).Count > 0;
        }
        public static string FormatXml(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXml);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }
            return sb.ToString();
        }
    }
}
