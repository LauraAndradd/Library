using Library.Application.Models;
using Library.Core.Entities;
using Library.Infrastructure.Persistence;
using MediatR;

namespace Library.Application.Commands.AddLoanCommand
{
    public class AddLoanHandler : IRequestHandler<AddLoanCommand, ResultViewModel>
    {
        private readonly LibraryDbContext _context;

        public AddLoanHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan(
                userId: request.UserId,
                bookId: request.BookId,
                loanDate: request.LoanDate,
                dueDate: request.DueDate,
                returnDate: null,
                isReturned: false
            );

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return new ResultViewModel(true, "Loan created successfully!");
        }
    }
}
