using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public class TimerClient
    {
        public static void Test()
        {
            Timer t = new Timer();
            t.Expired += x => Console.WriteLine($"Timer elapsed  : {x}");

            t.Expired += delegate (DateTime x)
            {
                Console.WriteLine($"Timer elapsed 2: {x}");
            };
            t.Interval = 500;
            t.Start();
        }
    }
}
