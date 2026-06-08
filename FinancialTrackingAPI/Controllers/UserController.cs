using FinancialTrackingAPI.Dtos.User;
using FinancialTrackingAPI.Helpers;
using FinancialTrackingAPI.Interfaces;
using FinancialTrackingAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTrackingAPI.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var users = await _repository.GetUsersAsync(query);
            return Ok(users.Select(u => u.ToResponse()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToResponse());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var user = await _repository.CreateAsync(request.ToRequest());
            return CreatedAtAction(
                nameof(GetById),
                new { id = user.UserId },
                user.ToResponse()
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserRequest request)
        {
            var user = await _repository.UpdateAsync(id, request.ToRequest());
            if (user == null) return NotFound();
            return Ok(user.ToResponse());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}