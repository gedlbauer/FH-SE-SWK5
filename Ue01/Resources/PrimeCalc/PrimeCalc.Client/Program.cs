using Newtonsoft.Json;
using PrimeCalc.Client;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PrimeCalc
{
    class Program
    {
        private const int MAX = 100;

        private static void PrintPrimesAsJson()
        {
            int n = 0;
            var primes = new List<int>();
            for (int i = 2; i <= MAX; i++)
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
            PrintPrimesAsJson();
            TestResources();
        }

        private static void TestResources()
        {
            Console.WriteLine($"UICulture={Thread.CurrentThread.CurrentUICulture}: ProgramName:{Resources.GetString("program-name")}");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-AT");
            Console.WriteLine($"UICulture={Thread.CurrentThread.CurrentUICulture}: ProgramName:{Resources.GetString("program-name")}");
        }
    }
}
