using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxForumSyndicate.Types
{
    /// <summary>
    /// This class represents the Channel tag in a RSS feed.
    /// </summary>
    public class Channel
    {
        //Mandatory
        public string Description { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }

        //Optional
        public string Category { get; set; }
        public string Cloud { get; set; }
        public string Copyright { get; set; }
        public string Docs { get; set; }
        public string Generator { get; set; }
        public string Image { get; set; }
        public string Language { get; set; }
        public string Lastbuilddate { get; set; }
        public string Managingeditor { get; set; }
        public string Pubdate { get; set; }
        public string Rating { get; set; }
        public string Skipdays { get; set; }
        public string Skiphuors { get; set; }
        public string Textinput { get; set; }
        public string Ttl { get; set; }
        public string Webmaster { get; set; }
    }
}
