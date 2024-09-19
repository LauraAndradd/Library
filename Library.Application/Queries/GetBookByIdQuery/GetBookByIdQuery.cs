using Library.Application.Models;
using MediatR;

namespace Library.Application.Queries.GetBookByIdQuery
{
    public class GetBookByIdQuery : IRequest<ResultViewModel<BookViewModel>>
    {
        public int BookId { get; set; }
    }
}
