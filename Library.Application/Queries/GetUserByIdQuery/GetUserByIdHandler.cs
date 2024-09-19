using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.GetUserByIdQuery
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly LibraryDbContext _context;

        public GetUserByIdHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return new ResultViewModel<UserViewModel>(null, false, "User not found.");
            }

            var userViewModel = new UserViewModel(user.Id, user.Username, user.Email);
            return new ResultViewModel<UserViewModel>(userViewModel, true);
        }
    }
}
