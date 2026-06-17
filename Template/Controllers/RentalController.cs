using Microsoft.AspNetCore.Mvc;
using Template.DTO;

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
            try
            {
                var created = await _service.CreateAsync(clientDto);
                return Created("", created);
            }
            catch (InvalidOperationException e)
            {
               return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpPut]
        [Route("/rentals/blocked-status")]
        public async Task<IActionResult> UpdateBlockedStatus([FromBody] UpdateRentalStatusDto dto)
        {
            var updated = await _service.UpdateRentalStatus(dto);
            if (updated == null)
                return Ok();

            return Ok(updated);
        }

        [HttpPut]
        [Route("/rentals/maintenance-status")]
        public async Task<IActionResult> UpdateMaintananceStatus([FromBody] UpdateVehicleStatusDto dto)
        {
            var updated = await _service.UpdateRentalByVehicleStatus(dto);
            if (updated == null)
                return Ok();

            return Ok(updated);
        }
    }
}