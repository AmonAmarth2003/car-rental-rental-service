using Microsoft.AspNetCore.Mvc;
using Template.DTO;
using Template.Entities;
using Template.Enums;
using Template.Services;

namespace Template.Controllers
{
    [ApiController]
    [Route("rentals")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _service;

        public RentalController(IRentalService service)
        {
            _service = service;
        }

        // GET /Rentals
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        // POST /Rentals
        [HttpPost]
        public async Task<IActionResult> Post(CreateRentalDto clientDto)
        {
            var created = await _service.CreateAsync(clientDto);
            return Created("", created);
        }

        [HttpPut]
        [Route("/rentals/blocked-status")]
        public async Task<IActionResult> UpdateBlockedStatus([FromBody] UpdateRentalStatusDto dto)
        {
            var updated = await _service.UpdateRentalStatusDto(dto);
            if (updated == null)
                return Ok();

            return Ok(updated);
        }
    }
}