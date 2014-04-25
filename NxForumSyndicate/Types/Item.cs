using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxForumSyndicate.Types
{
    public class Item
    {
        //Mantory
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        //Optional
        public string Author { get; set; }
        public string Category { get; set; }
        public string Comments { get; set; }
        public string Enclosure { get; set; }
        public string Guid { get; set; }
        public string PubDate { get; set; }
        public string Source { get; set; }
    }
}
