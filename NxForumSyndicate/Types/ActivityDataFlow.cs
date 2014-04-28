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
    public class ActivityDataFlow
    {
        /// <summary>
        /// Location where we get the information to convert to RSS.
        /// </summary>
        const string endpoint = @"http://forum2.nexon.net/activity.php";

        /// <summary>
        /// Get information current on the source.
        /// </summary>
        /// <returns>Activity Data Flow</returns>
        public static ActivityDataFlow GetCurrentActivityDataFlow()
        {
            using (var client = new WebClient())
                return new ActivityDataFlow() 
                {
                    Content = client.DownloadString(endpoint),
                    Time = DateTime.Now
                };
        }

        /// <summary>
        /// Information recieved from the source 
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// The time information was recieved from the source
        /// </summary>
        public  DateTime Time { get; set; }
        
        /// <summary>
        /// Parses through content and returns a collection of each variable in the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ActivityDataElement> GetAcivityDataElements()
        {
            //regex patterns
            string pattern2 = "<li class=\"activitybit forum_post\">";
            string pattern3 = "<li class=\"activitybit forum_thread\">";
            string pattern4 = "</li>";

            //Find location of the first post and the first thread.
            int post = Content.IndexOf(pattern2);
            int thread = Content.IndexOf(pattern3);

            //Find which one appears first.
            Content = Content.Substring(((post < thread) ? post : thread));

            
            for (int Start = 0, End = 0; Start != -1 && End != -1; Content = Content.Substring(End + pattern4.Length))
            {
                //Find index of the first post and first post.
                post = Content.IndexOf(pattern2);
                thread = Content.IndexOf(pattern3);
                //Determine which comes first, thread or post and find the index.
                Start = (post < thread) ? post : thread;
                //Find the index of the ending delimiter
                End = Content.IndexOf(pattern4);

                //If the value of the index of the start and end of data is valid create and return the activityDataElement.
                if (Start != -1 && End != -1)
                    yield return new ActivityDataElement() { Content = Content.Substring(Start, (End + pattern4.Length - Start)) };
            }
        }
    }
}
