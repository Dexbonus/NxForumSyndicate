using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NxForumSyndicate.Types
{
    /// <summary>
    /// This represents an entire RSS document
    /// </summary>
    public class Syndication
    {
        public String Version { get; set; }
        public List<Channel> Channels { get; set; }

        public Syndication()
        {
            Version = String.Empty;
            Channels = new List<Channel>();
        }
         
        public XmlDocument ToXmlDocument()
        {
            XmlDocument document = new XmlDocument();
            var documentNode = document.CreateElement("rss");
            var versionAttr = document.CreateAttribute("version");            
            versionAttr.Value = Version.ToString();

            foreach(var channel in Channels)
                documentNode.AppendChild(CreateRssChannel(document, channel));

            document.AppendChild(documentNode);
            return document;
        }

        static XmlElement CreateRssChannel(XmlDocument doc, Channel channel)
        {
            Object value;
            var channelNode = doc.CreateElement("channel");
            
            

            foreach (var property in channel.GetType().GetProperties())
                if ((value = property.GetValue(channel, null)) is String)
                    if ((((string)value) ?? string.Empty) != string.Empty)
                    {
                        var e = doc.CreateElement(property.Name.ToLower());
                        e.InnerText = value.ToString();
                        channelNode.AppendChild(e);
                    }
            foreach (var i in channel.ItemCollection)
                channelNode.AppendChild(CreateRssItem(doc, i));
            return channelNode;
        }

        static XmlElement CreateRssItem(XmlDocument document, Item item)
        {
            Object value;
            var itemNode = document.CreateElement("item");
            foreach (var property in item.GetType().GetProperties())
                if ((value = property.GetValue(item, null)) is string)
                    if ((((string)value) ?? string.Empty) != string.Empty)
                    {
                        var element = document.CreateElement(property.Name.ToLower());
                        element.InnerText = value.ToString();
                        itemNode.AppendChild(element);
                    }
            return itemNode;
        }
    }
}
