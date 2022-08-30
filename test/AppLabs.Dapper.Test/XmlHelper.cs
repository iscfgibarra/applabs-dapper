using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace ServiciosCFDI.Portal.Entities.Utils
{
    public class XmlHelper
    {
        /// <summary>
        /// Limpia el texto xml, si no es válido despues de la limpieza retorna un vacio.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string Clean(string xml)
        {
            //var aux = xml.Replace("&lt;", "<").Replace("&gt;", ">");
            var aux = RemoveUtfHead(xml);
            
            if (!IsValidXmlString(aux))
            {
                aux = RemoveInvalidXmlChars(aux);
            }

            var token = GetBetween(aux, "Token=", "Version=");

            if (!string.IsNullOrEmpty(token))
            {
                aux = aux.Replace(token, "\"\" ");
            }

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(aux);
                return aux;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

       

        private static bool IsValidXmlString(string text)
        {
            try
            {
                XmlConvert.VerifyXmlChars(text);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private static string RemoveInvalidXmlChars(string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }


        private static string GetBetween(string source, string startWord, string endWord)
        {
            int Start, End;
            if (source.Contains(startWord) && source.Contains(endWord))
            {
                Start = source.IndexOf(startWord, 0) + startWord.Length;
                End = source.IndexOf(endWord, Start);
                return source.Substring(Start, End - Start);
            }

            return string.Empty;
        }

        public static string RemoveUtfHead(string xml)
        {
            
            var aux = xml.Replace("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>", string.Empty);
            aux = aux.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty);
            return aux;
        }

        public static XmlDocument RemoveTimeAttributes(XmlDocument doc)
        {
            var root = doc.GetElementsByTagName("LoggerObject").Item(0);
            root.Attributes.RemoveNamedItem("StartTime");
            root.Attributes.RemoveNamedItem("LogTime");
            root.Attributes.RemoveNamedItem("Difference");
            return doc;
        }

        public static string GetMessageAttribute(XmlDocument doc)
        {
            string retval = string.Empty;

            var root = doc.GetElementsByTagName("LoggerObject").Item(0);

            retval = root.Attributes["Message"]?.Value;
            return retval;
        }

        public static string GetErrorMessage(XmlDocument doc)
        {
            string retval = string.Empty;

            var errorMessages = doc.GetElementsByTagName("ErrorMessages").Item(0);
            var errorMessage = errorMessages?.FirstChild;


            retval = errorMessage?.ChildNodes[0].InnerText + "-" + errorMessage?.ChildNodes[1].InnerText;

            return retval;
        }

        public static string XmlDocToString(XmlDocument doc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                var retval = stringWriter.GetStringBuilder().ToString();
                return RemoveUtfHead(retval);
            }
        }
    }
}
