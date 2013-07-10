using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

namespace Winsoft.Framework.MonitorProxy
{
    internal class XMLUtil
    {
        public static XmlDocument LoadXMLFile(string arquivoXML)
        {
            var doc = new XmlDocument();
          //  XmlNodeList nodeList;
            try
            {
                doc.Load(arquivoXML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doc;
        }

        public static XmlNodeList ReadXMLNodeList(string arquivoXML, string xPath)
        {
            //' xmlPath como "//Item/Valor"
            var doc = new XmlDocument();
            XmlNodeList nodeList;
            try
            {
                doc = LoadXMLFile(arquivoXML);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            nodeList = doc.SelectNodes(xPath);

            return nodeList;
        }

        public static XmlNodeList ReadXMLNodeList(XmlDocument objXMLDocument, string strXPath)
        {
            //' strNode como "//Item/Valor"

            return objXMLDocument.SelectNodes(strXPath);
        }

        public static string ReadXMLNode(XmlDocument objXMLDocument, string strXPath)
        {
            try
            {

                if (objXMLDocument.SelectSingleNode(strXPath) != null)
                {
                    var __text = objXMLDocument.SelectSingleNode(strXPath).InnerText;
                    return (!String.IsNullOrEmpty(__text) ? __text : string.Empty);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ReadXMLNode(XmlNode objXMLNode, string strXPath)
        {
            try
            {
                if (objXMLNode.SelectSingleNode(strXPath) != null)
                {
                    var __text = objXMLNode.SelectSingleNode(strXPath).InnerText;
                    return (!String.IsNullOrEmpty(__text) ? __text : string.Empty);
                }
                else
                {
                    // ' O Node que se espera localizar e extrair para devolver o conteudo nao existe no node informado
                    throw new Exception("'SingleNode' [" + strXPath + "] nao localizado no node XML.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool EhUmXMLBemFormado(string XML)
        {
            var objXML = new XmlDocument();
            var _return = false;
            try
            {
                objXML.LoadXml(XML);
                _return = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXML = null;
            }
            return _return;
        }
    }
}
