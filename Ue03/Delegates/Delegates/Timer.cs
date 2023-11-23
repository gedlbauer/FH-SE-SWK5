using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delegates
{
    public delegate void ExpiredEventHandler(DateTime signaledTime);
    public class Timer
    {
        private const int DEFAULT_INTERVAL = 1000;
        private Thread ticker;
        public int Interval { get; set; } = DEFAULT_INTERVAL;

        public event ExpiredEventHandler Expired;

        public Timer()
        {
            ticker = new Thread(OnTick);
        }

        private void OnTick()
        {
            while (true)
            {
                Expired?.Invoke(DateTime.Now);
                Thread.Sleep(Interval);
            }
        }

        public void Start() => ticker.Start();
    }
}
