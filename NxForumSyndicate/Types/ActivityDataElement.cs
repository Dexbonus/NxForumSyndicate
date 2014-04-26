using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NxForumSyndicate.Types
{
    class ActivityDataElement
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
        public string Excerpt { get; private set; }
        
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
                ///ensures we don't have null pointer exceptions;
                content = value ?? String.Empty;
                if (content != String.Empty)
                {
                    //if the value is not null and empty then it must be valid (since this is a private class)
                    Avatar = GetAvatarValue(content);
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
            EntryType = ActivityDataElementType.Unknown;
        }
        /// <summary>
        /// raw data content.
        /// </summary>
        String content;
        String GetAvatarValue(String value)
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

        String GetTitleValue(String Value)
        {
            //todo implemenet this.
            //Int32 sDist = 5, eDist = 1;
            //String titlePattern = "<div class=\"title\">[\\s\\S]*?(</div>)";
            //String linkTagStart = string.Empty;

            return null;
        }

    }
}
