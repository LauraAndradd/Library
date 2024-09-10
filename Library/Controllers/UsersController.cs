using Library.Entities;
using Library.Models;
using Library.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public UsersController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserInputModel request)
        {
            if (request == null)
                return BadRequest("Invalid user.");

            var user = new User(request.Username, request.Email);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully!");
        }
    }
}
