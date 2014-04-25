using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.IO;


namespace NxForumSyndicate.Types
{
    class ActivityDataFlow
    {
        const string endpoint = @"http://forum2.nexon.net/activity.php";

        public static ActivityDataFlow GetCurrentActivityDataFlow()
        {
            using (var client = new WebClient())
                return new ActivityDataFlow() 
                {
                    Content = client.DownloadString(endpoint),
                    Time = DateTime.Now
                };
        }

        public string Content { get; set; }
        public  DateTime Time { get; set; }
        
        
        public IEnumerable<ActivityDataElement> GetAcivityDataElements()
        {
            string pattern2 = "<li class=\"activitybit forum_post\">";
            string pattern3 = "<li class=\"activitybit forum_thread\">";
            string pattern4 = "</li>";

            int post = Content.IndexOf(pattern2);
            int thread = Content.IndexOf(pattern3);

            Content = Content.Substring(((post < thread) ? post : thread));

            for (int Start = 0, End = 0; Start != -1 && End != -1; Content = Content.Substring(End + pattern4.Length))
            {
                post = Content.IndexOf(pattern2);
                thread = Content.IndexOf(pattern3);

                Start = (post < thread) ? post : thread;
                End = Content.IndexOf(pattern4);

                if (Start != -1 && End != -1)
                    yield return new ActivityDataElement() { Content = Content.Substring(Start, (End + pattern4.Length - Start)) };
            }
        }
    }
}
