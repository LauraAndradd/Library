using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Queries.GetLoansByUserQuery
{
    public class GetLoansByUserHandler : IRequestHandler<GetLoansByUserQuery, ResultViewModel<List<LoanViewModel>>>
    {
        private readonly LibraryDbContext _context;

        public GetLoansByUserHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<LoanViewModel>>> Handle(GetLoansByUserQuery request, CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .Where(l => l.UserId == request.UserId)
                .ToListAsync();

            var loanViewModels = loans.Select(LoanViewModel.FromEntity).ToList();

            return new ResultViewModel<List<LoanViewModel>>(loanViewModels);
        }
    }
}
