using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.RemoveBookCommand
{
    public class RemoveBookHandler : IRequestHandler<RemoveBookCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public RemoveBookHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.BookId);
            if (book == null)
            {
                return ResultViewModel.Error("Book not found.");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
