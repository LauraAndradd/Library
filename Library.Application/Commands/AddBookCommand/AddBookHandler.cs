using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.AddBookCommand
{
    public class AddBookHandler : IRequestHandler<AddBookCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public AddBookHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new(request.Title, request.Author, request.PublicationYear);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
