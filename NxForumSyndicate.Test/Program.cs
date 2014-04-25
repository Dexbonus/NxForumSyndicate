using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NxForumSyndicate;
namespace NxForumSyndicate.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var Manager = SyndicationManager.Instance;
            var ElementStrings = Manager.GetDataFlowElementStrings();
            Console.WriteLine(ElementStrings.Last());
            Console.ReadLine();
        }
    }
}
