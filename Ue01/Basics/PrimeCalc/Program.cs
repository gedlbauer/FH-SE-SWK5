using System;

namespace PrimeCalc
{
    class Program
    {
        private const int MAX = 100;
        static void Main(string[] args)
        {
            int n = 0;
            for (int i = 2; i <= MAX; i++)
            {
                if (PrimeChecker.IsPrime(i))
                {
                    n++;
                    Console.WriteLine($"{i} is prime");
                }
            }
            Console.WriteLine($"Number of prime numbers in [2,{MAX}]: {n}");
        }
    }
}
