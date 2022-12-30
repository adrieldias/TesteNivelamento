using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Lista todas as contas correntes existentes no banco de dados
        /// </summary>
        /// <returns>As contas correntes existentes no banco de dados</returns>
        /// <response code="200">Retorna todas as contas correntes existentes no banco de dados</response>
        [HttpGet]
        public async Task<List<ContaCorrente>> GetContaCorrenteListAsync()
        {
            var ContaCorrente = await _mediator.Send(new GetContaCorrenteListQuery());

            return ContaCorrente;
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente
        /// </summary>
        /// <param name="contaCorrenteId">Identificação da conta corrente</param>
        /// <returns>O saldo da conta corrente</returns>
        /// <response code="200">Retorna o saldo da conta corrente</response>
        /// <response code="400">Retorna a descrição e o tipo da falha</response> 
        [HttpGet("consultar-saldo")]
        public async Task<IActionResult> ConsultarSaldoAsync(string contaCorrenteId)
        {
            var response = await _mediator.Send(new ConsultarSaldoContaCorrenteQuery() { Id = contaCorrenteId });
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}