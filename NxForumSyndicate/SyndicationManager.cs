using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using NxForumSyndicate.Types;

namespace NxForumSyndicate
{
    /// <summary>
    /// All of the data  from the RSS feed should be retrieved from a singleton.
    /// </summary>
    public sealed class SyndicationManager
    {
        static readonly SyndicationManager instance = new SyndicationManager();

        public static SyndicationManager Instance { get { return instance; } }
        public const int DefaultTimer = 30000;        
      

        //Settings
        public Int32 Interval
        {
            get
            {
                return Convert.ToInt32(timer.Interval);
            }
            set
            {
                timer.Interval = value;
            }
        }

        bool pollDragonNest;
        bool pollMapleStory;
        bool pollVindictus;
        bool pollAtlantica;
        bool pollCombatArms;
        bool pollMabinogi;

        //Functional Variables
        Timer timer;
        ICollection<ActivityDataElement> ItemCollection;

        public XmlDocument GetSyndiciation()
        {
            var syndication = new Syndication();

            Channel mainChannel = new Channel();
            mainChannel.Title = "Nexon Forums";
            mainChannel.Language = "en";
            mainChannel.Description = "All discussions on nexon forums";
            mainChannel.Link = "http://forum2.nexon.net";

            foreach (var element in ItemCollection)
                mainChannel.ItemCollection.Add(element.ToItem());

            syndication.Channels.Add(mainChannel);
            return syndication.ToXmlDocument();

        }
        
        private SyndicationManager()
        {
            ItemCollection = new List<ActivityDataElement>();
            PollActivityDataFlow();

            timer = new Timer();
            timer.Interval = DefaultTimer;
            timer.AutoReset = true;
            timer.Elapsed += (s,e)=> PollActivityDataFlow();
            timer.Start();    
        }

        public void PollActivityDataFlow()
        {
            foreach (var element in ActivityDataFlow.GetCurrentActivityDataFlow().GetAcivityDataElements())
                if(ItemCollection.FirstOrDefault(p => p.Author == element.Author && p.Excerpt == element.Excerpt)== null)
                    ItemCollection.Add(element);
            ItemCollection.OrderBy(p => p.Time);
        }


        public XmlDocument GetXmlForGame(GameType type)
        {
            throw new NotImplementedException();
        }
    }
}
