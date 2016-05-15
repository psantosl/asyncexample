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
            ThreadPool.SetMaxThreads(1, 1);

            Task.Factory.StartNew(() =>
                {
                    Request t = new Request("req0");

                    t.ProcessRequest("req0");
                }
            );

            Task.Factory.StartNew(() =>
                {
                    Request t = new Request("req1");

                    t.ProcessRequest("req1");
                }
            );

            while (true)
            {
                Console.WriteLine("Type a name and then ENTER to schedule a new request");
                string order = Console.ReadLine();

                Task.Factory.StartNew(() =>
                    {
                        Request t = new Request(order);

                        t.ProcessRequest(order);
                    }
                );
            }
        }

        class Request
        {
            internal Request(string name)
            {
                mName = name;
            }

            internal async void ProcessRequest(string name)
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
    }
}
