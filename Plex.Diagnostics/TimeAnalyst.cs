using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Plex.Diagnostics
{
    public class TimeAnalyst
    {
        static Dictionary<String, Task> tasks = new Dictionary<String, Task>();

        //Create
        static public void CreateTask(String n)
        {
            tasks.Add(n,new Task());
        }

        //Read
        static public Task GetTask(String n)
        {
            return tasks[n];
        }

        static public void SaveResults()
        {
            FileStream output = new FileStream("TimeAnalystResults.txt", FileMode.Create);
            foreach (KeyValuePair<String, Task> kvp in tasks)
            {
                Byte[] b = Encoding.ASCII.GetBytes(kvp.Key + " : " + kvp.Value.ToString() + " Ticks\n");
                output.Write(b, 0, b.Length);
            }
            output.Close();
        }

        //Update
        static public void StartTask(String n)
        {
            if (!tasks.ContainsKey(n))
                CreateTask(n);
            tasks[n].Start();
        }

        static public void StopTask(String n)
        {
            tasks[n].Stop();
        }

        //delete
        static public void RemoteTask(String n)
        {
            tasks.Remove(n);
        }

        
    }
}
