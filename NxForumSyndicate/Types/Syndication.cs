using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxForumSyndicate.Types
{
    /// <summary>
    /// This represents an entire RSS document
    /// </summary>
    public class Syndication
    {
        public string Version { get; set; }
        public List<Channel> Channels { get; set; }

        public Syndication()
        {
            Channels = new List<Channel>();
        }
    }
}
