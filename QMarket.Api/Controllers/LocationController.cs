using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using QMarket.Api.Interfaces;
using QMarket.Api.Models;
using QMarket.Api.ViewModels;

namespace QMarket.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<CustomerController> _logger;

        public LocationController(ILocationRepository locationRepository, ILogger<CustomerController> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            return await _locationRepository.GetAllAsync();
        } 
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocationAsync(int id)
        {
            var existingModel = await _locationRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound();
            }
            return existingModel;
        } 
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocationAsync(LocationViewModel model)
        {
            // var brand = await _customerRepository.GetModelAsync(model);
            // if(brand is not null)
            // {
            //     return BadRequest($"Brand {brandName} already exists.");
            // }
            var newModel = new Location();
            newModel.LocationName = model.Name;
            newModel.XCord = model.XCord;
            newModel.YCord = model.YCord;
            newModel.Rsid = model.Rsid;
            int id = await _locationRepository.CreateAsync(newModel);
            newModel.LocationId = id;
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} : Retrieved {newModel.LocationId}");
            return CreatedAtAction(nameof(GetLocationAsync), new { id = newModel.LocationId }, newModel);
        }
    }
}