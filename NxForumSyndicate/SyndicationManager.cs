using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using NxForumSyndicate.Types;

namespace NxForumSyndicate
{
    /// <summary>
    /// All of the data  from the RSS feed should be retrieved from a singleton.
    /// </summary>
    public class SyndicationManager
    {
        public const int DefaultTimer = 30000;
        public static SyndicationManager Instance { get; private set; }
      

        //Settings
        int interval;

        //Functional Variables
        ActivityDataFlow dataFlow;
        Timer timer;


        //Presently here for testing.
        public IEnumerable<string> GetDataFlowElementStrings()
        {
            foreach (var element in dataFlow.GetAcivityDataElements())
                yield return element.Content;
        }

        
        static SyndicationManager()
        {
            Instance = new SyndicationManager();
        }

        private SyndicationManager()
        {
            //Get First Set of DataFlow
            dataFlow = ActivityDataFlow.GetCurrentActivityDataFlow();

            //Set timer to get subsequent flows of data. This is to cache data from the service. 
            //It is possible to do this because there is a limit to posting frequency.
            timer = new Timer();
            timer.Interval = DefaultTimer;
            timer.AutoReset = true;
            timer.Elapsed += (s, e) => dataFlow = ActivityDataFlow.GetCurrentActivityDataFlow();
            timer.Start();
        }
    }
}
