using System;
using System.Collections.Generic;
using System.IO;

namespace Plex.Diagnostics
{
    public class TimeAnalyst
    {
        static Dictionary<String, Task> tasks = new Dictionary<String, Task>();

        static public IEnumerable<Task> Tasks
        {
            get
            {
                return tasks.Values;
            }
        }

        //Create
        static public void CreateTask(String n)
        {
            tasks.Add(n, new Task() { Name = n });
        }

        //Read
        static public Task GetTask(String n)
        {
            return tasks[n];
        }

        static public void SaveResults(Stream stream)
        {
            using (var Writer = new StreamWriter(stream))
                foreach (var v in Tasks)
                    Writer.WriteLine(v.Name + " : " + v.Duration + " Ticks | " + v.GetMilliseconds() + " Milliseconds ");
        }

        static public void SaveResults(TextWriter writer)
        {
            foreach (var v in Tasks)
                writer.WriteLine(v.Name + " : " + v.Duration + " Ticks | " + v.GetMilliseconds() + " Milliseconds ");
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
        static public void RemoveTask(String n)
        {
            tasks.Remove(n);
        }
    }
}
