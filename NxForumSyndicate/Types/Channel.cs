using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NxForumSyndicate.Types
{
    /// <summary>
    /// This class represents the Channel tag in a RSS feed.
    /// </summary>
    public class Channel
    {
        //Mandatory
        public String Description { get; set; }
        public String Link { get; set; }
        public String Title { get; set; }

        //Optional
        public String Category { get; set; }
        public String Cloud { get; set; }
        public String Copyright { get; set; }
        public String Docs { get; set; }
        public String Generator { get; set; }
        public String Image { get; set; }
        public String Language { get; set; }
        public String LastBuildDate { get; set; }
        public String ManagingEditor { get; set; }
        public String PubDate { get; set; }
        public String Rating { get; set; }
        public String SkipDays { get; set; }
        public String SkipHours { get; set; }
        public String TextInput { get; set; }
        public String Ttl { get; set; }
        public String Webmaster { get; set; }

        public List<Item> ItemCollection { get; set; }

        public Channel()
        {
            ItemCollection = new List<Item>();
        }

    }
}
