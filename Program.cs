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

            for (int i = 0; i < 4; ++i)
            {
                string name = i.ToString();
                Task.Factory.StartNew(() => { new Request("req" + name).ProcessRequest(); });
            }

            while (true)
            {
                Console.WriteLine("Type a name and then ENTER to schedule a new request");
                string order = Console.ReadLine();

                Task.Factory.StartNew(() =>
                    {
                        Request t = new Request(order);

                        t.ProcessRequest();
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

            internal async void ProcessRequest()
            {
                int ini = Environment.TickCount;

                WriteLine(mName, "Starting request");

                WriteLine(mName, "Do heavy calculation ...");

                Thread.Sleep(1000);

                WriteLine(mName, "Done. {0} ms", Environment.TickCount - ini);

                WriteLine(mName, "Sleeping to do async");

                await Task.Delay(10000);

                //Thread.Sleep(10000);

                WriteLine(mName, "Big async operation. Request terminated. {0} ms since start to complete", Environment.TickCount - mStart);
            }

            void WriteLine(string requestName, string format, params object[] args)
            {
                string s = string.Format(format, args);

                Console.WriteLine("{0} - thId - {1} - {2}",
                    requestName, Thread.CurrentThread.ManagedThreadId, s);
            }

            string mName;
        }

        static int mStart = Environment.TickCount;
    }
}
