using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.BackgroundServices;
using OrderManagement.Api.Dtos;
using OrderManagement.Domain;
using OrderManagement.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;
        private readonly IMapper mapper;
        private readonly UpdateChannel updateChannel;

        public CustomersController(IOrderManagementLogic logic, IMapper mapper, UpdateChannel updateChannel)
        {
            this.logic = logic;
            this.mapper = mapper;
            this.updateChannel = updateChannel;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomers(Rating? rating)
        {
            IEnumerable<Customer> customers;
            if (rating == null)
            {
                customers = await logic.GetCustomersAsync();
            }
            else
            {
                customers = await logic.GetCustomersByRatingAsync(rating.Value);
            }
            return mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid customerId)
        {
            var customer = await logic.GetCustomerAsync(customerId);
            if (customer is null)
            {
                return NotFound();
            }
            return mapper.Map<CustomerDto>(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerCreationDto customerDto)
        {
            if (customerDto.Id != Guid.Empty && await logic.CustomerExistsAsync(customerDto.Id))
            {
                return Conflict();
            }

            Customer customer = mapper.Map<Customer>(customerDto);
            await logic.AddCustomerAsync(customer);
            return CreatedAtAction(actionName: nameof(GetCustomerById), routeValues: new { customerId = customer.Id }, value: mapper.Map<CustomerDto>(customer));
        }

        [HttpPost("{customerId}/update-totals")]
        public async Task<ActionResult> UpdateCustomerTotals(Guid customerId)
        {
            if (!await logic.CustomerExistsAsync(customerId))
            {
                return NotFound();
            }

            // Synchronous implementation
            // UpdateTotalRevenueAsync blocks => high processing time
            // Client must wait a long time for response
            // await logic.UpdateTotalRevenueAsync(customerId);
            // return NoContent();

            // Asynchronous implementation
            // Processing is handed to hosted service via channel
            // Client gets response immediately
            if(await updateChannel.AddUpdateTaskAsync(customerId))
            {
                return Accepted();
            } else
            {
                return new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            }
        }

    }
}
