using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Queries.GetLoansByUserQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IMediator _mediator;

        public LoansController(ILoanService loanService, IMediator mediator)
        {
            _loanService = loanService;
            _mediator = mediator;
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLoansByUser(int userId)
        {
            var query = new GetLoansByUserQuery { UserId = userId };
            var result = await _mediator.Send(query);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result.Data);
        }
    }
}
