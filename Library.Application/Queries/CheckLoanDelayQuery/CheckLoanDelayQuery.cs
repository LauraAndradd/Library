using Library.Application.Models;
using MediatR;

namespace Library.Application.Queries.CheckLoanDelayQuery
{
    public class CheckLoanDelayQuery : IRequest<ResultViewModel<string>>
    {
        public int LoanId { get; set; }
    }
}
