using System;
using System.Xml;
using ServiciosCFDI.Portal.Entities.Utils;

namespace AppLabs.Dapper.Test
{
    public class LogMetadata
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string RFC { get; set; }

        public string ServiceId { get; set; }

        public Guid? RequestId { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string ErrorCode { get; set; }

        public TimeSpan? Time { get; set; }

        


        public string Xml 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Message)) return string.Empty;

                var aux = XmlHelper.Clean(Message);

                if (string.IsNullOrEmpty(aux)) return string.Empty;

                var doc = new XmlDocument();
                doc.LoadXml(aux);
                doc = XmlHelper.RemoveTimeAttributes(doc);
                return "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>" + XmlHelper.XmlDocToString(doc);
                return string.Empty;
            }
        }

        private string _messageAttribute;

        public string MessageAttribute
        {
            get
            {
                if (!string.IsNullOrEmpty(_messageAttribute)) return _messageAttribute;

                var aux = XmlHelper.RemoveUtfHead(this.Xml);
                if (string.IsNullOrEmpty(aux)) return _messageAttribute;

                var doc = new XmlDocument();
                doc.LoadXml(aux);
                _messageAttribute = XmlHelper.GetMessageAttribute(doc);
                return _messageAttribute;
            }
        }

        private string _errorAttribute;
        public string ErrorAttribute
        {
            get
            {
                if (!string.IsNullOrEmpty(_errorAttribute)) return _errorAttribute;

                var aux = XmlHelper.RemoveUtfHead(this.Xml);
                if (string.IsNullOrEmpty(aux)) return _errorAttribute;

                var doc = new XmlDocument();
                doc.LoadXml(aux);
                _errorAttribute = XmlHelper.GetErrorMessage(doc);
                return _errorAttribute;
            }
        }

    }
}
