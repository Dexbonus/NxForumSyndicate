using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Plex.Diagnostics;

namespace Plex.Diagnostics.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeAnalyst.CreateTask("Hello");
            TimeAnalyst.StartTask("Hello");

            Console.WriteLine("Hello World");

            TimeAnalyst.StopTask("Hello");
            TimeAnalyst.SaveResults(Console.Out);
            Console.ReadLine();
            
        }
    }
}
