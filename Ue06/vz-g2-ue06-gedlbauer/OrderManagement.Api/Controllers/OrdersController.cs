using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Domain;
using OrderManagement.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;
        private readonly IMapper mapper;
        public OrdersController(IOrderManagementLogic logic, IMapper mapper)
        {
            this.logic = logic;
            this.mapper = mapper;
        }

        [HttpGet("customers/{customerId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersOfCustomer(Guid customerId)
        {
            if (!await logic.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }
            return Ok((await logic.GetOrdersOfCustomerAsync(customerId)).Select(x => x.ToDto()));
        }

        [HttpGet("orders/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid orderId)
        {
            if (!await logic.OrderExistsAsync(orderId))
            {
                return NotFound(orderId);
            }

            var order = await logic.GetOrderAsync(orderId);
            return mapper.Map<OrderDto>(order);
        }

        [HttpPost("customers/{customerId}/orders")]
        public async Task<ActionResult<OrderDto>> CreateOrderForCustomer(Guid customerId, [FromBody] OrderCreationDto orderDto)
        {
            if (orderDto.Id != Guid.Empty && await logic.OrderExistsAsync(orderDto.Id))
            {
                return Conflict();
            }

            Order order = mapper.Map<Order>(orderDto);
            await logic.AddOrderForCustomerAsync(customerId, order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = order.Id }, mapper.Map<OrderDto>(order));
        }
    }
}
