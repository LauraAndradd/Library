using Library.Application.Models;
using MediatR;

namespace Library.Application.Commands.ReturnLoanCommand
{
    public class ReturnLoanCommand : IRequest<ResultViewModel>
    {
        public int LoanId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
