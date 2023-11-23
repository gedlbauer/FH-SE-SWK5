﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagement.Domain;

namespace OrderManagement.Logic
{
    public class OrderManagementLogic : IOrderManagementLogic
    {
        private const int PROCESSING_TIME_TOTAL_REVENUE_CUSTOMER = 2000;
        private const int PROCESSING_TIME_TOTAL_REVENUES = 2000;

        private static readonly object lockObject = new object();

        private static readonly IDictionary<Guid, DbCustomer> customers = new Dictionary<Guid, DbCustomer>();
        private static readonly IDictionary<Guid, DbOrder> orders = new Dictionary<Guid, DbOrder>();

        private DbCustomer EnsureCustomerExists(Guid customerId)
        {
            if (!customers.TryGetValue(customerId, out var dbCustomer))
            {
                throw new ArgumentException($"Customer with id {customerId} does not exist");
            }

            return dbCustomer;
        }

        private DbOrder EnsureOrderExists(Guid orderId)
        {
            if (!orders.TryGetValue(orderId, out var dbOrder))
            {
                throw new ArgumentException($"Order with id {orderId} does not exist");
            }

            return dbOrder;
        }

        public Task<bool> CustomerExistsAsync(Guid customerId)
        {
            lock (lockObject)
            {
                return Task.FromResult(customers.TryGetValue(customerId, out var _));
            }
        }

        public Task<Customer> GetCustomerAsync(Guid customerId)
        {
            lock (lockObject)
            {
                customers.TryGetValue(customerId, out var dbCustomer);
                return Task.FromResult(dbCustomer?.ToCustomer());
            }
        }

        public Task AddCustomerAsync(Customer customer)
        {
            lock (lockObject)
            {
                if (customer.Id == Guid.Empty)
                {
                    customer.Id = Guid.NewGuid();
                }

                if (!customers.TryAdd(customer.Id, customer.ToDbCustomer()))
                {
                    throw new ArgumentException($"Customer with id {customer.Id} already exists");
                }

                return Task.CompletedTask;
            }
        }

        public Task AddOrderForCustomerAsync(Guid customerId, Order order)
        {
            lock (lockObject)
            {
                var dbCustomer = EnsureCustomerExists(customerId);

                if (order.Id == Guid.Empty)
                {
                    order.Id = Guid.NewGuid();
                }
                order.Customer = dbCustomer.ToCustomer();
                orders.Add(order.Id, order.ToDbOrder());
                return Task.CompletedTask;
            }
        }

        public Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            lock (lockObject)
            {
                return Task.FromResult(customers.Remove(customerId));
            }
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            lock (lockObject)
            {
                return Task.FromResult(customers.Values.Select(c => c.ToCustomer()));
            }
        }

        public Task<IEnumerable<Customer>> GetCustomersByRatingAsync(Rating rating)
        {
            lock (lockObject)
            {
                return Task.FromResult(customers.Values.Where(c => c.Rating == rating).Select(c => c.ToCustomer()));
            }
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            lock (lockObject)
            {
                var dbCustomer = EnsureCustomerExists(customer.Id);
                dbCustomer.Name = customer.Name;
                dbCustomer.ZipCode = customer.ZipCode;
                dbCustomer.City = customer.City;
                dbCustomer.Rating = customer.Rating;

                return Task.CompletedTask;
            }
        }

        public Task<bool> OrderExistsAsync(Guid orderId)
        {
            lock (lockObject)
            {
                return Task.FromResult(orders.TryGetValue(orderId, out var _));
            }
        }

        public Task<Order> GetOrderAsync(Guid orderId)
        {
            lock (lockObject)
            {
                var dbOrder = EnsureOrderExists(orderId);
                var customer = customers[dbOrder.CustomerId].ToCustomer();
                return Task.FromResult(dbOrder.ToOrder(customer));
            }
        }

        public Task<IEnumerable<Order>> GetOrdersOfCustomerAsync(Guid customerId)
        {
            lock (lockObject)
            {
                var dbCustomer = EnsureCustomerExists(customerId);
                var customer = dbCustomer.ToCustomer();
                return Task.FromResult(orders.Values.Where(order => order.CustomerId == customerId)
                                                    .Select(dbOrder => dbOrder.ToOrder(customer)));
            }
        }

        public Task UpdateOrderAsync(Order order)
        {
            lock (lockObject)
            {
                var dbOrder = EnsureOrderExists(order.Id);
                dbOrder.Article = order.Article;
                dbOrder.OrderDate = order.OrderDate;
                dbOrder.TotalPrice = order.TotalPrice;

                return Task.CompletedTask;
            }
        }

        public Task<bool> DeleteOrderAsync(Guid orderId)
        {
            lock (lockObject)
            {
                return Task.FromResult(orders.Remove(orderId));
            }
        }

        private decimal UpdateTotalRevenueInternal(DbCustomer customer)
        {
            var total = orders.Values.Where(c => c.CustomerId == customer.Id).Sum(order => order.TotalPrice);
            return customer.TotalRevenue = total;
        }

        public async Task<decimal> UpdateTotalRevenueAsync(Guid customerId)
        {
            decimal total = 0m;
            lock (lockObject)
            {
                var dbCustomer = EnsureCustomerExists(customerId);
                total = UpdateTotalRevenueInternal(dbCustomer);
            }

            await Task.Delay(2000); // simulate long processing time
            return await Task.FromResult(PROCESSING_TIME_TOTAL_REVENUE_CUSTOMER);
        }

        public async Task UpdateTotalRevenuesAsync()
        {
            lock (lockObject)
            {
                foreach (var customer in customers.Values)
                {
                    UpdateTotalRevenueInternal(customer);
                }
            }

            await Task.Delay(PROCESSING_TIME_TOTAL_REVENUES); // simulate long processing time
        }

        static OrderManagementLogic()
        {

            var customer1 = new Customer { Id = new Guid("cccccccc-0000-0000-0000-111111111111"), Name = "Stefan Mayr", ZipCode = 1010, City = "Wien", Rating = Rating.A };
            customers.Add(customer1.Id, customer1.ToDbCustomer());

            var customer2 = new Customer { Id = new Guid("cccccccc-0000-0000-0000-222222222222"), Name = "Susanne Huber", ZipCode = 4020, City = "Linz", Rating = Rating.B };
            customers.Add(customer2.Id, customer2.ToDbCustomer());

            var customer3 = new Customer { Id = new Guid("cccccccc-0000-0000-0000-333333333333"), Name = "Franz Himmelpfortsleitner", ZipCode = 6020, City = "Innsbruck", Rating = Rating.C };
            customers.Add(customer3.Id, customer3.ToDbCustomer());

            var customer4 = new Customer { Id = new Guid("cccccccc-0000-0000-0000-444444444444"), Name = "Maria Blümchen", ZipCode = 1010, City = "Wien", Rating = Rating.A };
            customers.Add(customer4.Id, customer4.ToDbCustomer());



            Order order = null;

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000111111"), Article = "Dell Monitor", OrderDate = new DateTimeOffset(new DateTime(2021, 3, 4)), TotalPrice = 528.3m, Customer = customer1 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000222222"), Article = "Saugroboter", OrderDate = new DateTimeOffset(new DateTime(2021, 5, 15)), TotalPrice = 522.5m, Customer = customer1 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000333333"), Article = "Suface Book 3", OrderDate = new DateTimeOffset(new DateTime(2021, 12, 12)), TotalPrice = 2258.3m, Customer = customer1 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000444444"), Article = "Apple Watch", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 400.33m, Customer = customer1 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000555555"), Article = "Huawei P30", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 238.5m, Customer = customer2 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000666666"), Article = "Nikkon D80", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 855.3m, Customer = customer2 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000777777"), Article = "Blitzgerät", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 88.3m, Customer = customer3 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000888888"), Article = "Braun Ohrthermometer", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 45.25m, Customer = customer4 };
            orders.Add(order.Id, order.ToDbOrder());

            order = new Order { Id = new Guid("aaaaaaaa-0000-0000-0000-000000999999"), Article = "Sonos Bream 2", OrderDate = new DateTimeOffset(new DateTime(2021, 6, 23)), TotalPrice = 483.99m, Customer = customer4 };
            orders.Add(order.Id, order.ToDbOrder());
        }
    }
}
