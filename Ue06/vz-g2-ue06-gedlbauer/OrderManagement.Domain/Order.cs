using System;

namespace OrderManagement.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Article { get; set; }

        public decimal TotalPrice { get; set; }

        public Customer Customer { get; set; }
    }
}
