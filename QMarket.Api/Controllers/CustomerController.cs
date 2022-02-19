using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using QMarket.Api.ViewModels;

namespace QMarket.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerAsync(int id)
        {
            var existingModel = await _customerRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound();
            }
            return existingModel;
        } 
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomerAsync(CustomerViewModel model)
        {
            // var brand = await _customerRepository.GetModelAsync(model);
            // if(brand is not null)
            // {
            //     return BadRequest($"Brand {brandName} already exists.");
            // }
            var newModel = new Customer();
            newModel.FirstName = model.FirstName;
            newModel.LastName = model.LastName;
            int id = await _customerRepository.CreateAsync(newModel);
            newModel.CustomerId = id;
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} : Retrieved {newModel.CustomerId}");
            return CreatedAtAction(nameof(GetCustomerAsync), new { id = newModel.CustomerId }, newModel);
        }
    }
}