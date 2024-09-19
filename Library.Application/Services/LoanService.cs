using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Commands.AddLoanCommand;
using Library.Application.Commands.ReturnLoanCommand;
using Library.Application.Queries.CheckLoanDelayQuery;
using MediatR;
using System.Threading.Tasks;
using Library.Application.Queries.GetLoansByUserQuery;

namespace Library.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IMediator _mediator;

        public LoanService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultViewModel> CreateLoanAsync(CreateLoanInputModel request)
        {
            if (request == null)
                return new ResultViewModel(false, "Invalid loan.");

            // Cria um comando para adicionar o empréstimo
            var command = new AddLoanCommand
            {
                UserId = request.UserId,
                BookId = request.BookId,
                LoanDate = request.LoanDate,
                DueDate = DateTime.Now.AddDays(14)
            };

            // Envia o comando para o MediatR e retorna o resultado
            return await _mediator.Send(command);
        }

        public async Task<ResultViewModel> RegisterReturnAsync(int loanId, DateTime returnDate)
        {
            // Cria um comando para registrar a devolução
            var command = new ReturnLoanCommand
            {
                LoanId = loanId,
                ReturnDate = returnDate
            };

            // Envia o comando para o MediatR e retorna o resultado
            return await _mediator.Send(command);
        }

        public async Task<ResultViewModel> CheckForDelayAsync(int loanId)
        {
            // Cria uma query para verificar se o empréstimo está em atraso
            var query = new CheckLoanDelayQuery { LoanId = loanId };

            // Envia a query para o MediatR e retorna o resultado
            return await _mediator.Send(query);
        }

        public async Task<ResultViewModel<List<LoanViewModel>>> GetLoansByUserAsync(int userId)
        {
            var query = new GetLoansByUserQuery { UserId = userId };

            return await _mediator.Send(query);
        }
    }
}
