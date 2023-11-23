using System;

namespace OrderManagement.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ZipCode { get; set; }

        public string City { get; set; }

        public Rating Rating { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
