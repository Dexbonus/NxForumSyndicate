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
        public String Title { get; set; }
        public String Link { get; set; }
        public String Description { get; set; }

        //Optional
        public String Author { get; set; }
        public String Category { get; set; }
        public String Comments { get; set; }
        public String Enclosure { get; set; }
        public String Guid { get; set; }
        public String PubDate { get; set; }
        public String Source { get; set; }
    }
}
