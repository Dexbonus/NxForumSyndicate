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
using System.Text.RegularExpressions;


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
            {
                //this method allows getting the content as xml instead of raw html with extra data we don't need (scripts, css, etc)
                client.Headers["X-Requested-With"] = "XMLHttpRequest";

                return new ActivityDataFlow()
                {
                    Content = Encoding.ASCII.GetString(client.UploadValues(endpoint,
                        new System.Collections.Specialized.NameValueCollection()
                        {
                            {"securitytoken", "guest"},
                            {"pp", "30"}, //per-page variable, doesn't seem to actually limit the ammount of results though
                            {"sortby", "recent"},
                            {"time", "anytime"},
                            {"show", "all"} //can be filtered by: all, photos or forums                   
                        })),
                    Time = DateTime.Now
                };                

                //return new ActivityDataFlow()
                //{
                //    Content = client.DownloadString(endpoint),
                //    Time = DateTime.Now
                //};
            }
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
            string pattern = "(?:<li class=\"activitybit forum_(?:post|thread))\">([\\s\\S]*?)(?:</li>)";
            foreach (Match match in Regex.Matches(Content, pattern))
                yield return new ActivityDataElement() { Content = match.Value };

      
        }
    }
}
