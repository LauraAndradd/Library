using Library.Entities;
using Library.Models;
using Library.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanInputModel request)
        {
            if (request == null)
                return BadRequest("Invalid loan.");

            var loan = new Loan(request.UserId, request.BookId, request.LoanDate, DateTime.Now.AddDays(14), null, false);

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return Ok("Loan registered successfully!");
        }

        [HttpPost("return")]
        public async Task<IActionResult> RegisterReturn(int loanId, DateTime returnDate)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return NotFound("Loan not found.");

            loan.MarkAsReturned(returnDate);

            await _context.SaveChangesAsync();

            return Ok("Return registered successfully!");
        }


        [HttpGet("check-delay/{loanId}")]
        public async Task<IActionResult> CheckForDelay(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                return NotFound("Loan not found.");

            var message = loan.IsReturned
                ? "Loan returned."
                : "Loan not returned yet.";

            return Ok(message);
        }
    }
}
