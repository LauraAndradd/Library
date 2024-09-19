using Library.Application.Models;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.ReturnLoanCommand
{
    public class ReturnLoanHandler : IRequestHandler<ReturnLoanCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public ReturnLoanHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans.FindAsync(request.LoanId);
            if (loan == null)
            {
                return new ResultViewModel(false, "Loan not found.");
            }

            loan.MarkAsReturned(request.ReturnDate);

            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Loan returned successfully!");
        }
    }
}
