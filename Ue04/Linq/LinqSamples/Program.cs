using LinqSamples.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            static void Print(IEnumerable<Customer> customers)
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer);
                }
            }
            var repo = new CustomerRepository();
            var customers = repo.GetCustomers();

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Kunden mit Jahresumsatz > 1 Mio");

            // V1: Fluent Syntax
            //var customersByRevenue = customers.Where(x => x.Revenue > 1_000_000)
            //    .OrderByDescending(x => x.Revenue);

            // V2: Query Expression
            var customersByRevenue = from x in customers
                                     where x.Revenue > 1_000_000
                                     orderby x.Revenue descending
                                     select x;
            Print(customersByRevenue);


            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Kunden, deren Name mit A/a beginnt");
            var customersByName = from x in customers
                                  where x.Name.StartsWith("a", StringComparison.InvariantCulture)
                                  select x;
            Print(customersByName);


            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Kunde mit vorgegebenem Namen");
            var customer = customers.FirstOrDefault(x => x.Name.Equals("manuyo", StringComparison.InvariantCultureIgnoreCase));
            Console.WriteLine($"Kunde 'manyo': {customer?.ToString() ?? "existiert nicht"}");


            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Kunden in Österreich");
            var customersByCountry = from x in customers
                                     where x.Location.Country == "Österreich"
                                     select x;
            Print(customersByCountry);


            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Top 3 umsatzstärkste Kunden");
            var largestCustomers = (from x in customers
                                    orderby x.Revenue descending
                                    select x).Take(3);
            Print(largestCustomers);

            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Durchschnittlicher Umastz von A Kunden");
            var revenueOfACustomers = customers.Where(x => x.Rating == Rating.A).Average(x => x.Revenue);
            Console.WriteLine($"{revenueOfACustomers:n2} Euro");

            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Gruppierung der Kunden nach Land");
            var customersGroupedByCountry = customers.GroupBy(x => x.Location.Country)
                .Select(x => new { Country = x.Key, Customers = x });
            foreach (var group in customersGroupedByCountry.OrderBy(x => x.Country))
            {
                Console.WriteLine(group.Country);
                foreach (var c in group.Customers)
                {
                    Console.WriteLine($"\t{c}");
                }
            }

            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Durchschnittlicher Umsatz je Land");
            var revenueByCountry = customers.GroupBy(x => x.Location.Country)
                .Select(x => new { Country = x.Key, Average = x.Average(y => y.Revenue) });
            foreach (var rc in revenueByCountry)
            {
                Console.WriteLine($"{rc.Country}: {rc.Average:n2}");
            }
        }
    }
}
