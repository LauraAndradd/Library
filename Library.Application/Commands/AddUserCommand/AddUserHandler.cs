using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.AddUserCommand
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public AddUserHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Username, request.Email);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResultViewModel(true, "User successfully added.");
        }
    }
}
