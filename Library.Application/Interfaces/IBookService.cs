using Library.Application.Models;

namespace Library.Application.Interfaces
{
    public interface IBookService
    {
        Task<ResultViewModel> AddBookAsync(CreateBookInputModel request);
        Task<ResultViewModel<List<BookViewModel>>> GetAllBooksAsync();
        Task<ResultViewModel<BookViewModel>> GetBookByIdAsync(int id);
        Task<ResultViewModel> RemoveBookAsync(int id);
    }
}