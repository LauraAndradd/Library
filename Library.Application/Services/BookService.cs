using Library.Application.Commands.AddBookCommand;
using Library.Application.Commands.RemoveBookCommand;
using Library.Application.Commands.UpdateBookCommand;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Queries.GetAllBooksQuery;
using Library.Application.Queries.GetBookByIdQuery;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        private readonly IMediator _mediator;

        public BookService(LibraryDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<List<BookViewModel>>> GetAllBooksAsync()
        {
            var query = new GetAllBooksQuery();
            return await _mediator.Send(query);
        }

        public async Task<ResultViewModel<BookViewModel>> GetBookByIdAsync(int id)
        {
            var query = new GetBookByIdQuery { BookId = id };
            var result = await _mediator.Send(query);

            return result;
        }


        public async Task<ResultViewModel> AddBookAsync(CreateBookInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid book.");

            var command = new AddBookCommand
            {
                Title = request.Title,
                Author = request.Author,
                PublicationYear = request.PublicationYear
            };

            return await _mediator.Send(command);
        }

        public async Task<ResultViewModel> UpdateBookAsync(int id, UpdateBookInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid book data.");

            var command = new UpdateBookCommand
            {
                BookId = id,
                Title = request.Title,
                Author = request.Author,
                PublicationYear = (int)request.PublicationYear
            };

            return await _mediator.Send(command);
        }

        public async Task<ResultViewModel> RemoveBookAsync(int id)
        {
            var command = new RemoveBookCommand { BookId = id };
            return await _mediator.Send(command);
        }
    }
}
