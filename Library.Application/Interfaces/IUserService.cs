using Library.Application.Models;

namespace Library.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResultViewModel> AddUserAsync(CreateUserInputModel request);
    }
}