using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLock
{
    class Program
    {
        static void Main()
        {
            var threads = new Thread[9];
            var acc = new Account(1000);

            for (var i = 0; i < 3; i++)
            {
                var t = new Thread(new ThreadStart(acc.DoWithdrawTransactions));
                threads[i*3] = t;
                //Thread.Sleep(100);

                t = new Thread(new ThreadStart(acc.DoDevideTransactions));
                threads[i*3+1] = t;
                //Thread.Sleep(100);

                t = new Thread(new ThreadStart(acc.DoAddTransactions));
                threads[i*3+2] = t;
                //Thread.Sleep(100);
            }

            for (var i = 0; i < 9; i++)
            {
                threads[i].Start();
            }
            Console.ReadLine();
        }
    }
}
