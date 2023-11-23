using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Dtos
{
    public static class DtoExtensions
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            if (customer is null) return null;
            return new CustomerDto
            {
                Id = customer.Id,
                City = customer.City,
                Name = customer.Name,
                Rating = customer.Rating,
                TotalRevenue = customer.TotalRevenue,
                ZipCode = customer.ZipCode
            };
        }

        public static Customer ToDomain(this CustomerDto customer)
        {
            if (customer is null) return null;
            return new Customer
            {
                Id = customer.Id,
                City = customer.City,
                Name = customer.Name,
                Rating = customer.Rating,
                TotalRevenue = customer.TotalRevenue,
                ZipCode = customer.ZipCode
            };
        }

        public static OrderDto ToDto(this Order order)
        {
            if(order is null) return null;
            return new OrderDto
            {
                Id = order.Id,
                Article = order.Article,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Customer = order.Customer.ToDto()
            };
        }
    }
}
