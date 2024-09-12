using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDbContext _context;

        public LoanService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> CreateLoanAsync(CreateLoanInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid loan.");

            var loan = new Loan(request.UserId, request.BookId, request.LoanDate, DateTime.Now.AddDays(14), null, false);

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Loan registered successfully!");
        }

        public async Task<ResultViewModel> RegisterReturnAsync(int loanId, DateTime returnDate)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return new ResultViewModel(false, "Loan not found.");

            loan.MarkAsReturned(returnDate);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Return registered successfully!");
        }

        public async Task<ResultViewModel> CheckForDelayAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return new ResultViewModel(false, "Loan not found.");

            var message = loan.IsReturned
                ? "Loan returned."
                : "Loan not returned yet.";

            return new ResultViewModel(true, message);
        }
    }
}
