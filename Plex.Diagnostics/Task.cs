using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Plex.Diagnostics
{
    public class Task
    {
        private Stopwatch timer;
        public long duration;

        public Task()
        {
            timer = new Stopwatch();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            duration = timer.ElapsedTicks;
        }

        public long GetMilliseconds()
        {
            return timer.ElapsedMilliseconds;
        }

        public override string ToString()
        {
            return timer.ElapsedTicks.ToString();
        }
    }

}
