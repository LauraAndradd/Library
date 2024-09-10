using Library.Entities;

namespace Library.Models
{
    public class LoanViewModel
    {
        public LoanViewModel(int bookId, int userId, DateTime loanDate)
        {
            BookId = bookId;
            UserId = userId;
            LoanDate = loanDate;
        }

        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }

        public static LoanViewModel FromEntity(Loan loan)
        {
            return new LoanViewModel(loan.BookId, loan.UserId, loan.LoanDate);
        }
    }
}
