using OrderManagement.Domain;
using System;

namespace OrderManagement.Logic
{
    internal class DbCustomer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ZipCode { get; set; }

        public string City { get; set; }

        public Rating Rating { get; set; }

        public decimal TotalRevenue { get; set; }

        public Customer ToCustomer() => new Customer
        {
            Id = this.Id,
            Name = this.Name,
            ZipCode = this.ZipCode,
            City = this.City,
            Rating = this.Rating,
            TotalRevenue = this.TotalRevenue
        };
    }

    internal static class CustomerExtensions
    {
        public static DbCustomer ToDbCustomer(this Customer customer) => new DbCustomer
        {
            Id = customer.Id,
            Name = customer.Name,
            ZipCode = customer.ZipCode,
            City = customer.City,
            Rating = customer.Rating,
            TotalRevenue = customer.TotalRevenue
        };
    }
}
