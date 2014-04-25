using System.Diagnostics;

namespace Plex.Diagnostics
{
    public class Task
    {
        private Stopwatch timer;

        public string Name { get; set; }
        public long Duration { get; private set; }

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
            Duration = timer.ElapsedTicks;
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
