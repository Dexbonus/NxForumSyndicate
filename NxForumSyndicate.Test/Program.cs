using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NxForumSyndicate;
namespace NxForumSyndicate.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            { 
                var Manager = SyndicationManager.Instance;
                var Ele = Manager.GetXml();

                using (var stream = new FileStream("syndication.xml", FileMode.Create))
                    new XmlSerializer(typeof(XmlDocument)).Serialize(stream, Ele);
                Console.WriteLine(Ele);
            }
            catch (Exception e) { 
                Console.Error.WriteLine(e.ToString());
            }
            finally
            {
                Console.ReadLine();

            }
        }
    }
}
