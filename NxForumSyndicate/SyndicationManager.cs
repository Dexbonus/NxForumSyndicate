using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using NxForumSyndicate.Types;

namespace NxForumSyndicate
{
    public class SyndicationManager
    {
        public const int DefaultTimer = 30000;
        public static SyndicationManager Instance { get; private set; }
      

        //Settings
        int interval;

        //Functional Variables
        ActivityDataFlow dataFlow;
        Timer timer;


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

            //Set timer to get subsequent flows of data
            timer = new Timer();
            timer.Interval = DefaultTimer;
            timer.AutoReset = true;
            timer.Elapsed += (s, e) => dataFlow = ActivityDataFlow.GetCurrentActivityDataFlow();
            timer.Start();
        }
    }
}
