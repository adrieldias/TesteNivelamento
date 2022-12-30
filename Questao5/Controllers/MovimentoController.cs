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
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Realiza a movimentação da conta corrente (Depósitos e Saques)
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Movimento/movimentar-conta-corrente
        ///     {
        ///        "idRequisicao": "db3df9fe-bc52-4ccd-98cc-162491f2e969",
        ///        "idContaCorrente": "ee32e180-d536-4e14-a5fb-25312947129d",
        ///        "valor": 10,
        ///        "tipoMovimento": "C"
        ///     }
        ///
        /// </remarks>
        /// <param name="movimentoRequestDto">Conjunto de informações necessárias para realizar a movimentação</param>
        /// <returns>O Id do movimento criado</returns>
        /// <response code="200">Retorna o Id do movimento criado</response>
        /// <response code="400">Retorna a descrição e o tipo da falha</response> 
        [HttpPost("movimentar-conta-corrente")]
        public async Task<IActionResult> AddMovimentoAsync(MovimentoRequestDto movimentoRequestDto)
        {
            var response = await _mediator.Send(new CreateMovimentoCommand(
                movimentoRequestDto.IdRequisicao,
                movimentoRequestDto.IdContaCorrente,
                movimentoRequestDto.Valor,
                movimentoRequestDto.TipoMovimento
                ));
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
            
        }
    }
}