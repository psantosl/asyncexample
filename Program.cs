using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAsyncProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerThread th = new WorkerThread("worker0");

            Thread t = new Thread(th.Run);
            t.Start();

            mQueue.Enqueue("req0");
            mQueue.Enqueue("req1");

            while (true)
            {
                Console.WriteLine("Type a name and then ENTER to schedule a new request");
                string order = Console.ReadLine();

                lock (mLock)
                {
                    mQueue.Enqueue(order);

                    Monitor.Pulse(mLock);
                }
            }
        }

        class WorkerThread
        {
            internal WorkerThread(string name)
            {
                mName = name;
            }

            internal void Run()
            {
                WriteLine(mName, "Starting worker thread");

                while (true)
                {
                    string order;

                    lock (mLock)
                    {
                        if (mQueue.Count == 0)
                        {
                            WriteLine(mName, "Sleeping to wait for request to enter");
                            Monitor.Wait(mLock);
                        }

                        order = mQueue.Dequeue();
                    }

                    ProcessRequest(order);

                    WriteLine(mName, "Returning to process loop");
                }
            }

            async void ProcessRequest(string name)
            {
                WriteLine(name, "Starting request");

                int ini = Environment.TickCount;

                WriteLine(name, "Do heavy calculation ...");

                Thread.Sleep(1000);

                WriteLine(name, "Done. {0} ms", Environment.TickCount - ini);

                WriteLine(name, "Sleeping to do async");

                await Task.Delay(10000);

                WriteLine(name, "Big async operation done");
            }

            void WriteLine(string requestName, string format, params object[] args)
            {
                string s = string.Format(format, args);

                Console.WriteLine("[{0}] - [thId: {1}] - {2}",
                    requestName, Thread.CurrentThread.ManagedThreadId, s);
            }

            string mName;
        }

        static Queue<string> mQueue = new Queue<string>();
        static object mLock = new object();
    }
}
