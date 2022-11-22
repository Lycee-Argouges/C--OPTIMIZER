using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPTIMIZER
{
    public class ServerClass
    {
        // The method that will be called when the thread is started.
        public void InstanceMethod()
        {
            PreventSleeping();
        }
        public void StopInstanceMethod()
        {
            NativeMethods.AllowSleep();
        }

        public static void StaticMethod()
        {
            //
        }

        private void PreventSleeping()
        {
            bool someBoolean = false;
            int numYields = 0;
            int waitTime = 0;

            // First task: SpinWait until someBoolean is set to true
            Task t1 = Task.Factory.StartNew(() =>
            {
                SpinWait sw = new SpinWait();
                while (!someBoolean)
                {
                    // The NextSpinWillYield property returns true if
                    // calling sw.SpinOnce() will result in yielding the
                    // processor instead of simply spinning.
                    if (sw.NextSpinWillYield) numYields++;
                    sw.SpinOnce();
                }

                // As of .NET Framework 4: After some initial spinning, SpinWait.SpinOnce() will yield every time.
                Console.WriteLine("SpinWait called {0} times, yielded {1} times", sw.Count, numYields);
            });

            // Second task: Wait 1000ms, then set someBoolean to true
            Task t2 = Task.Factory.StartNew(() =>
            {
                do
                {
                    NativeMethods.PreventNoScreen();
                    Thread.Sleep(10000);
                    waitTime++;
                } while (waitTime < 180 && !NotifyZone.someFlag);
                someBoolean = true;
            });

            // Wait for tasks to complete
            Task.WaitAll(t1, t2);
        }
    }
}
