using Library.Application.Models;
using MediatR;

namespace Library.Application.Commands.RemoveBookCommand
{
    public class RemoveBookCommand : IRequest<ResultViewModel>
    {
        public int BookId { get; set; }
    }
}
