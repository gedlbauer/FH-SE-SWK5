using OrderManagement.Domain;
using System;

namespace OrderManagement.Logic
{
    internal class DbOrder
    {
        public Guid Id { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Article { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid CustomerId { get; set; }

        public Order ToOrder(Customer customer = null) => new Order
        {
            Id = this.Id,
            OrderDate = this.OrderDate,
            Article = this.Article,
            TotalPrice = this.TotalPrice,
            Customer = customer
        };
    }

    internal static class OrderExtensions
    {
        public static DbOrder ToDbOrder(this Order order) => new DbOrder
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            Article = order.Article,
            TotalPrice = order.TotalPrice,
            CustomerId = order.Customer.Id
        };
    }
}
