using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;

namespace Library.Application.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _context;

        public UserService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> AddUserAsync(CreateUserInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid user.");

            var user = new User(request.Username, request.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "User registered successfully!");
        }
    }
}
