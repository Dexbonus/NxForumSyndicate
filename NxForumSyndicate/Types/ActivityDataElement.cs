using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NxForumSyndicate.Types
{
    public class ActivityDataElement
    {
        /// <summary>
        /// The base URL of all links (since links in the source are all relatively linked)
        /// </summary>
        const string BaseUrl = @"http://forum2.nexon.net/";
        /// <summary>
        /// This returns the URL to the poster's image
        /// </summary>
        public string Avatar { get; private set; }
        /// <summary>
        /// This shows what type of bost the activity data element is
        /// </summary>
        public ActivityDataElementType EntryType { get; private set; }
        /// <summary>
        /// This shows when the post or thread was made
        /// </summary>
        public DateTime Time { get; private set; }
        /// <summary>
        /// This provides the URL to the post or thread
        /// </summary>
        public String Link { get; private set; }
        /// <summary>
        /// Partial text from the post or thread.
        /// </summary>
        public String Excerpt { get; private set; }
        /// <summary>
        /// The game the post applies to.
        /// </summary>
        public GameType Game { get; private set; }
        public String Title { get; private set; }
        public String Author { get; private set; }
        /// <summary>
        /// Raw text from the ActivityDataElement delimiters
        /// </summary>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if ((content = value ?? String.Empty) != String.Empty)
                {
                    //if the value is not null and empty then it must be valid (since this is a private class)
                    Link = GetLinkValue(content);
                    Title = GetTitleValue(content);
                    Avatar = GetAvatarValue(content);
                    Excerpt = GetExcerptValue(content);
                    Time = GetTimeValue(content);
                    Game = GetGameTypeValue(content);
                    Author = GetAuthorValue(content);
                }
            }
        }

        /// <summary>
        /// Constructor, makes sure that we don't have any unexpected data.
        /// </summary>
        public ActivityDataElement()
        {
            Link = String.Empty;
            Avatar = String.Empty;
            content = String.Empty;
            Excerpt = String.Empty;
            Time = DateTime.MinValue;
            Author = String.Empty;
            EntryType = ActivityDataElementType.Unknown;
        }
        /// <summary>
        /// raw data content.
        /// </summary>
        String content;
        public String GetAvatarValue(String value)
        {
            //Initialize and instansiate variables.
            Int32 sDist = 5, eDist = 1;
            String imgPattern = "src=\"[^\"]*\"";
            String divPattern = "(<div class=\"avatar\">)[\\s\\S]*?(</div>)";

            //Extract the div tag with avator from the element content and the img url from the div element.
            String result = Regex.Match(Regex.Match(value, divPattern).Value, imgPattern).Value;
            
            //todo Check if its a default image or custom. The links are different for the two.

            //separate content from the delimiters
            result = result.Substring(sDist, result.Length - eDist - sDist);
            return BaseUrl + result;
        }

        public String GetTitleValue(String value)
        {
            Int32 sDist = 22, eDist = 6;
            String titlePattern = "<div class=\"title\">[\\s\\S]*?(</div>)";
            String linkTagStart = "</?a[\\s\\S]*?(>)";
            String linkTagEnd = String.Empty;

            //Use regex to extract div from Content, then remove all link tags from div and excess whitespace.
            var cleanOutput = Regex.Replace(Regex.Replace(Regex.Match(value, titlePattern).Value, linkTagStart, ""), @"\s+", " ").Trim();
            //Cut out delimiters
            var result = cleanOutput.Substring(sDist, cleanOutput.Length - eDist - sDist);
            return result;
        }

        public String GetExcerptValue(String value)
        {
            Int32 sDist = 21, eDist = 6;
            String DivPatten = "<div class=\"excerpt\">[\\s\\S]*?(</div>)";
            var result = Regex.Match(value, DivPatten).Value;
            result = result.Substring(sDist, result.Length - eDist - sDist);
            return result;
        }

        public String GetLinkValue(String value)
        {
            Int32 sDist = 9, eDist = 2;
            String DivPattern = "<div class=\"fulllink\">[\\s\\S]*?(</div>)";
            String LinkPattern = "<a href=\"[\\s\\S]*?(\">)";

            var result = Regex.Match(Regex.Match(value, DivPattern).Value, LinkPattern).Value;
            result = result.Substring(sDist, result.Length - eDist - sDist);

            return BaseUrl + result;
        }

        public DateTime GetTimeValue(String value)
        {
            string DayPattern = "(<span class=\"date\">)(Today|Yesterday)[\\s\\S]*?(</span>)";
            string TimePattern = "(<span class=\"time\">)[\\s\\S]*?(</span>)";
            Int32 sDist = 19, eDist = 7;
            DateTime time;

            var DayMatch = Regex.Match(value, DayPattern);
            if (!DayMatch.Success)
                throw new Exception("Could not extract the Date");

            var result = Regex.Match(value, TimePattern).Value;
            result = result.Substring(sDist, result.Length - eDist - sDist);

            time = DayMatch.Value.Contains("Today") ?
                DateTime.Parse(result) :
                DateTime.Today.Add(DateTime.Parse(result) - DateTime.Today - TimeSpan.FromDays(1));
             
            return time;
        }

        public String GetAuthorValue(string value)
        {
            string AuthorPattern = "(?:<div class=\"title\">[\\s\\S]*?<a[\\s\\S]*?>)([\\s\\S]*?)(?:</a>[\\s\\S]*?</div>)";
            string result = Regex.Match(value, AuthorPattern).Groups[1].Value;
            return result;
        }

        public GameType GetGameTypeValue(String value)
        {        
            //not that great at regex so you might want to review this
            String DivPattern = "<div class=\"title\">[\\s\\S]*?(</div>)";
            String LinkPattern = "<a href=\"[\\s\\S]*?(\">)";

            var result = Regex.Matches(Regex.Match(value, DivPattern).Value, LinkPattern)[2].Value;
            result = Regex.Match(result, "\\?(.*?)\\-").Value;
            var id = Int32.Parse(result.Replace("?", "").Replace("-", ""));            

            if (GameData.DragonNest.Contains(id))
                return GameType.DragonNest;
            else if (GameData.CombatArms.Contains(id))
                return GameType.CombatArms;
            else if (GameData.Atlantica.Contains(id))
                return GameType.Atlantica;
            else if (GameData.Mabinogi.Contains(id))
                return GameType.Mobinogi;
            else if (GameData.MapleStory.Contains(id))
                return GameType.MapleStory;
            else if (GameData.Vindictus.Contains(id))
                return GameType.Vindictus;

            return GameType.Unknown;
        }

        public Item ToItem()
        {
            Item item = new Item();
            item.Link = Link;
            item.Title = Title;
            item.Description = Excerpt;
            item.PubDate = Time.ToLongDateString() + " " + Time.ToLongTimeString();
            item.Author = Author;
            //using this for the game type since it wasn't being used :p
            item.Category = Game.ToString();

            return item;
        }
    }
}
