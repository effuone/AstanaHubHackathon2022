using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using QMarket.Api.ViewModels;

namespace QMarket.Api.Controllers
{
    [ApiController]
    [Route("api/couriers")]
    public class CourierRepository : ControllerBase
    {
        private readonly ICourierRepository _courierRepository;
        private readonly ILogger<CustomerController> _logger;

        public CourierRepository(ICourierRepository courierRepository, ILogger<CustomerController> logger)
        {
            _courierRepository = courierRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Courier>> GetCouriersAsync()
        {
            return await _courierRepository.GetAllAsync();
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<Courier>> GetCustomerAsync(int id)
        {
            var existingModel = await _courierRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound();
            }
            return existingModel;
        } 
        [HttpPost]
        public async Task<ActionResult<Courier>> PostCustomerAsync(CourierViewModel model)
        {
            // var brand = await _customerRepository.GetModelAsync(model);
            // if(brand is not null)
            // {
            //     return BadRequest($"Brand {brandName} already exists.");
            // }
            var newModel = new Courier();
            newModel.CompanyName = model.CompanyName;
            int id = await _courierRepository.CreateAsync(newModel);
            newModel.CourierId = id;
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} : Retrieved {newModel.CourierId}");
            return CreatedAtAction(nameof(GetCustomerAsync), new { id = newModel.CourierId }, newModel);
        }
    }
}