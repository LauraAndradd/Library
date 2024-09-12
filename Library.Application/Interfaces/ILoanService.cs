using Library.Application.Models;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<ResultViewModel> CheckForDelayAsync(int loanId);
        Task<ResultViewModel> CreateLoanAsync(CreateLoanInputModel request);
        Task<ResultViewModel> RegisterReturnAsync(int loanId, DateTime returnDate);
    }
}