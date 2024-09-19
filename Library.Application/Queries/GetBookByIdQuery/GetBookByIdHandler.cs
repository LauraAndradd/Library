using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.GetBookByIdQuery
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, ResultViewModel<BookViewModel>>
    {
        private readonly LibraryDbContext _context;

        public GetBookByIdHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<BookViewModel>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .SingleOrDefaultAsync(b => b.Id == request.BookId);

            if (book is null)
            {
                return new ResultViewModel<BookViewModel>(null, false, "Book not found.");
            }

            var bookViewModel = new BookViewModel(book.Id, book.Title, book.Author, book.PublicationYear);
            return new ResultViewModel<BookViewModel>(bookViewModel);
        }
    }
}
