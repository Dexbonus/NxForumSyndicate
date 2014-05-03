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
        int interval;

        //Functional Variables
        ActivityDataFlow dataFlow;
        Syndication syndication;
        Timer timer;


        //Presently here for testing.
        public IEnumerable<string> GetDataFlowElementStrings()
        {
            foreach (var element in dataFlow.GetAcivityDataElements())
                yield return element.Content;
        }

        public IEnumerable<ActivityDataElement> GetDataFlowElement()
        {
            foreach (var element in dataFlow.GetAcivityDataElements())
                yield return element;
        }

        public XmlDocument GetXml()
        {
           return syndication.ToXmlDocument();
        }
        
        private SyndicationManager()
        {
            //Get First Set of DataFlow
            dataFlow = ActivityDataFlow.GetCurrentActivityDataFlow();            
            syndication = new Syndication();
            
            Channel mainChannel = new Channel();
            mainChannel.Title = "Nexon Forums";
            mainChannel.Language = "en";
            mainChannel.Description = "All discussions on nexon forums";
            mainChannel.Link = "http://forum2.nexon.net";

            foreach (var element in dataFlow.GetAcivityDataElements())
                mainChannel.ItemCollection.Add(element.ToItem());

            syndication.Channels.Add(mainChannel);

            //Set timer to get subsequent flows of data. This is to cache data from the service. 
            //It is possible to do this because there is a limit to posting frequency.
            timer = new Timer();
            timer.Interval = DefaultTimer;
            timer.AutoReset = true;
            timer.Elapsed += timer_Elapsed;
            timer.Start();            
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Sync();
        }

        public void Sync(){
            lock(dataFlow)
                lock(syndication)
                {
                    dataFlow = ActivityDataFlow.GetCurrentActivityDataFlow();
                    syndication = new Syndication();

                    Channel mainChannel = new Channel();
                    mainChannel.Title = "Nexon Forums";
                    mainChannel.Language = "en";
                    mainChannel.Description = "All discussions on nexon forums";
                    mainChannel.Link = "http://forum2.nexon.net";

                    foreach (var element in dataFlow.GetAcivityDataElements())
                        mainChannel.ItemCollection.Add(element.ToItem());

                    syndication.Channels.Add(mainChannel);
                }
        }

        public XmlDocument GetXmlForGame(GameType type)
        {
            var doc = syndication.ToXmlDocument();

            foreach (XmlNode node in doc.SelectNodes("//item"))
                if (node.SelectSingleNode("category").InnerText != type.ToString())
                    node.ParentNode.RemoveChild(node);

            return doc;             
        }
    }
}
