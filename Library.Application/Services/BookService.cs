using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> AddBookAsync(CreateBookInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid book.");

            var book = new Book(request.Title, request.Author, request.ISBN, request.PublicationYear);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Book registered successfully!");
        }

        public async Task<ResultViewModel<List<BookViewModel>>> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            var bookViewModels = books.Select(BookViewModel.FromEntity).ToList();
            return new ResultViewModel<List<BookViewModel>>(bookViewModels);
        }

        public async Task<ResultViewModel<BookViewModel>> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return new ResultViewModel<BookViewModel>(null, false, "Book not found.");

            var bookViewModel = BookViewModel.FromEntity(book);
            return new ResultViewModel<BookViewModel>(bookViewModel);
        }

        public async Task<ResultViewModel> RemoveBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return new ResultViewModel(false, "Book not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Book removed successfully!");
        }
    }
}
