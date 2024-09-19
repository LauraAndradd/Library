using Library.Application.Models;
using MediatR;

namespace Library.Application.Commands.AddLoanCommand
{
    public class AddLoanCommand : IRequest<ResultViewModel>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
