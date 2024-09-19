using Library.Application.Models;
using MediatR;

namespace Library.Application.Commands.AddBookCommand
{
    public class AddBookCommand : IRequest<ResultViewModel>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
    }
}
