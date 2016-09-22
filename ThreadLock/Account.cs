using System;
using System.Windows.Forms;

namespace ThreadLock
{

    class Account
    {

        private readonly object _withdrawLock = new object();
        private readonly object _addLock = new object();
        private readonly object _devideLock = new object();

        private int _balance;
        private int _operationNumber = 0;

        Random _rand = new Random();

        public Account(int amount)
        {
            _balance = amount;
        }

        private int Withdraw(int amount)
        {
            Console.WriteLine("START withdraw operation");
            // This condition never is true unless the lock statement
            // is commented out.
            if (_balance < 0)
            {
                throw new Exception("Negative Balance");
            }

            // Comment out the next line to see the effect of leaving out 
            // the lock keyword.
            lock (_devideLock)
            {
                lock (_withdrawLock)
                {
                    Console.WriteLine("==============Lock thread withdraw inside devide================");
                    if (_balance >= amount)
                    {
                        Console.WriteLine($"Balance before Withdrawal {++_operationNumber}:  " + _balance);
                        Console.WriteLine("Amount to Withdraw          : -" + amount);
                        _balance -= amount;
                        Console.WriteLine("Balance after Withdrawal    :  " + _balance);
                        Console.WriteLine($"FINISH withdraw operation {_operationNumber}\n");
                        return amount;
                    }
                    else
                    {
                        Console.WriteLine("Transaction withdraw rejected");
                        return 0; // transaction rejected
                    }
                }
            }
            
        }


        private int Add(int amount)
        {
            Console.WriteLine("START Add operation");
            // Comment out the next line to see the effect of leaving out 
            // the lock keyword.
            lock (_devideLock)
            {
                lock (_withdrawLock)
                {
                    Console.WriteLine("=================Lock thread add inside divide==================");
                    if (_balance >= amount)
                    {
                        Console.WriteLine($"Balance before Add {++_operationNumber}:  " + _balance);
                        Console.WriteLine("Amount to Add        :  " + amount);
                        _balance += amount;
                        Console.WriteLine("Balance after Add    :  " + _balance);
                        Console.WriteLine($"FINISH Add operation {_operationNumber}\n");
                        return amount;
                    }
                    else
                    {
                        Console.WriteLine("Transaction Add rejected");
                        return 0; // transaction rejected
                    }
                }

            }
            
        }


        private int Devide(int amount)
        {
            Console.WriteLine("START devide operation");
            // This condition never is true unless the lock statement
            // is commented out.
            if (_balance < 0 || _balance < amount)
            {
                throw new Exception("Negative Balance");
            }

            // Comment out the next line to see the effect of leaving out 
            // the lock keyword.
            lock (_devideLock)
            {
                Console.WriteLine("===============Lock thread devide=================");
                if (_balance >= amount)
                {
                    Console.WriteLine($"Balance before devide {++_operationNumber}:  " + _balance);
                    Console.WriteLine("Amount to devide        :  " + amount);
                    _balance /= amount;
                    Console.WriteLine("Balance after devide    :  " + _balance);
                    Console.WriteLine($"FINISH devide operation {_operationNumber}\n");
                    return amount;
                }
                else
                {
                    Console.WriteLine("Transaction devide rejected");
                    return 0; // transaction rejected
                }
            }
        }


        public void DoWithdrawTransactions()
        {
            for (var i = 0; i < 3; i++)
            {
                Withdraw(_rand.Next(1, 10));
            }
        }

        public void DoAddTransactions()
        {
            for (var i = 0; i < 3; i++)
            {
                Add(_rand.Next(1, 100));
            }
        }

        public void DoDevideTransactions()
        {
            for (var i = 0; i < 3; i++)
            {
                Devide(_rand.Next(1, 3));
            }
        }


    }

}