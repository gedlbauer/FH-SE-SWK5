using HashDictionary.Impl;
using System;
using System.Collections.Generic;

namespace HashDictionary.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestDictionary();
        }

        private static void TestDictionary()
        {
            var cities = new HashDictionary<string, int>();
            try
            {
                cities.Add("Hofkirchen", 1_300);
                cities["Linz"] = 180_000;
                cities["Linz"] = 200_000;
                cities.Add("Hagenberg", 2_500);
                cities.Add("Hagenberg", 2_500);
            } catch (ArgumentException e)
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}");
            }

            try
            {
                Console.WriteLine($"cities[\"Hofkirchen\"] = {cities["Hofkirchen"]}");
                Console.WriteLine($"cities[\"Linz\"] = {cities["Linz"]}");
                Console.WriteLine($"cities[\"Hagenberg\"] = {cities["Hagenberg"]}");
                Console.WriteLine($"cities[\"Graz\"] = {cities["Graz"]}");
            } catch (KeyNotFoundException e)
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}");
            }
            PrintDictionary(cities);
            var keys = cities.Keys;
            var values = cities.Values;
            cities.Remove("Hofkirchen");
            cities.Remove("Hagenberg");
            cities.Remove("Linz");
            Console.WriteLine(keys);
        }
        private static void PrintDictionary<K, V>(IDictionary<K, V> dictionary)
        {
            foreach (var item in dictionary)
            {
                Console.WriteLine($"({item.Key} -> {item.Value})");
            }
        }
    }
}
