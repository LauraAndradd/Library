using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.UpdateBookCommand
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public UpdateBookHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.BookId);
            if (book == null)
            {
                return ResultViewModel.Error("Book not found.");
            }

            book.Update(request.Title, request.Author, request.PublicationYear);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
