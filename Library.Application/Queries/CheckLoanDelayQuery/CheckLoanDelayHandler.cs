using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.CheckLoanDelayQuery
{
    public class CheckLoanDelayHandler : IRequestHandler<CheckLoanDelayQuery, ResultViewModel<string>>
    {
        private readonly LibraryDbContext _context;

        public CheckLoanDelayHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<string>> Handle(CheckLoanDelayQuery request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans
                .SingleOrDefaultAsync(l => l.Id == request.LoanId, cancellationToken);

            if (loan == null)
                return new ResultViewModel<string>(null, false, "Loan not found.");

            if (loan.IsReturned)
                return new ResultViewModel<string>("Loan already returned.", true);

            if (loan.DueDate < DateTime.Now)
                return new ResultViewModel<string>("Loan is overdue.", true);
            else
                return new ResultViewModel<string>("Loan is within the return date.", true);
        }
    }
}
