using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string Article { get; set; }

        public decimal TotalPrice { get; set; }

        public CustomerDto Customer { get; set; }
    }
}
