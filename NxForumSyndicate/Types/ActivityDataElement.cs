using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NxForumSyndicate.Types
{
    class ActivityDataElement
    {
        const string BaseUrl = @"http://forum2.nexon.net/";

        public string Avatar { get; private set; }
        public ActivityDataElementType EntryType { get; private set; }
        public DateTime Time { get; private set; }
        public String Link { get; private set; }
        public string Excerpt { get; private set; }
        
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value ?? String.Empty;
                if (content != String.Empty)
                {
                    Avatar = GetAvatarValue(content);
                }
            }
        }

        public ActivityDataElement()
        {
            Link = String.Empty;
            Avatar = String.Empty;
            content = String.Empty;
            Excerpt = String.Empty;
            Time = DateTime.MinValue;
            EntryType = ActivityDataElementType.Unknown;
        }

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

    }
}
