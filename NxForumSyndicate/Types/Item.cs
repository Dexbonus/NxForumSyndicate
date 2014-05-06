using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxForumSyndicate.Types
{
    /// <summary>
    /// This class represents the Item tag in a RSS feed.
    /// </summary>
    public class Item
    {
        //Mantory
        public String title { get; set; }
        public String link { get; set; }
        public String description { get; set; }

        //Optional
        public String author { get; set; }
        public String category { get; set; }
        public String comments { get; set; }
        public String enclosure { get; set; }
        public String guid { get; set; }
        public String pubDate { get; set; }
        public String source { get; set; }
    }
}
