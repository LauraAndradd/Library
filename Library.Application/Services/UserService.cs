using Library.Application.Commands.AddUserCommand;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Queries.GetUserByIdQuery;
using MediatR;

namespace Library.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultViewModel> AddUserAsync(CreateUserInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid user.");

            var command = new AddUserCommand(request.Username, request.Email);
            var result = await _mediator.Send(command);

            return result;
        }

        public async Task<ResultViewModel<UserViewModel>> GetUserByIdAsync(int userId)
        {
            var query = new GetUserByIdQuery { UserId = userId };
            var result = await _mediator.Send(query);

            return result;
        }
    }
}
