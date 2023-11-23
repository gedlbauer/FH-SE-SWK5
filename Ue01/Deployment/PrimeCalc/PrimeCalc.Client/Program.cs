using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PrimeCalc
{
    class Program
    {
        private const int DEFAULT = 100;

        private static void PrintPrimesAsJson(int limit)
        {
            int n = 0;
            var primes = new List<int>();
            for (int i = 2; i <= limit; i++)
            {
                if (PrimeChecker.IsPrime(i))
                {
                    n++;
                    primes.Add(i);
                }
            }
            string json = JsonConvert.SerializeObject(new { Count = n, Primes = primes });
            Console.WriteLine(json);
        }

        static void Main(string[] args)
        {
            var limit = args.Length == 1 ? int.Parse(args[0]) : DEFAULT;
            PrintPrimesAsJson(limit);
        }
    }
}
