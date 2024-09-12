using Library.Application.Models;
using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanInputModel request)
        {
            var result = await _loanService.CreateLoanAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("return")]
        public async Task<IActionResult> RegisterReturn(int loanId, DateTime returnDate)
        {
            var result = await _loanService.RegisterReturnAsync(loanId, returnDate);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpGet("check-delay/{loanId}")]
        public async Task<IActionResult> CheckForDelay(int loanId)
        {
            var result = await _loanService.CheckForDelayAsync(loanId);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
