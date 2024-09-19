using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.GetAllBooksQuery
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<BookViewModel>>>
    {
        private readonly LibraryDbContext _context;

        public GetAllBooksHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<BookViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.ToListAsync();

            var bookViewModels = books.Select(b => new BookViewModel(b.Id, b.Title, b.Author, b.PublicationYear)).ToList();

            return new ResultViewModel<List<BookViewModel>>(bookViewModels);
        }
    }
}
