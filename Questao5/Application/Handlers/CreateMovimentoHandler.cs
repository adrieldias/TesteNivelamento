using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Database.UoW;
using Questao5.Domain.Resources;
using Questao5.Domain.Dtos;

namespace Questao5.Application.Handlers
{
    public class CreateMovimentoHandler : IRequestHandler<CreateMovimentoCommand, ResponseDto>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMovimentoHandler(IMovimentoRepository MovimentoRepository, 
            IIdempotenciaRepository idempotenciaRepository, 
            IUnitOfWork unitOfWork,
            IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = MovimentoRepository;
            _idempotenciaRepository = idempotenciaRepository;
            _unitOfWork = unitOfWork;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ResponseDto> Handle(CreateMovimentoCommand command, CancellationToken cancellationToken)
        {
            var idempotencia = await _idempotenciaRepository.GetAsync(command.IdRequisicao);
            if(idempotencia != null)
            {
                return new ResponseDto(true, idempotencia.Resultado);
            }

            var contaCorrente = await _contaCorrenteRepository.GetAsync(command.IdContaCorrente);
            if (contaCorrente == null)
                return new ResponseDto(false, MessagesResource.INVALID_ACCOUNT);
            if (!contaCorrente.IsValid())
                return new ResponseDto(false, MessagesResource.INACTIVE_ACCOUNT);

            var movimento = new Movimento()
            {
                Id = Guid.NewGuid().ToString(),
                IdContaCorrente = command.IdContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = command.TipoMovimento,
                Valor = command.Valor,
            };
            if (!movimento.IsValid())
            {
                return new ResponseDto(false, JsonConvert.SerializeObject(movimento.ValidationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }
            
            var id = await _movimentoRepository.AddAsync(movimento);
            if (id != null) {
                idempotencia = new Idempotencia
                {
                    Id = command.IdRequisicao,
                    Requisicao = JsonConvert.SerializeObject(command),
                    Resultado = id,
                };
                await _idempotenciaRepository.AddAsync(idempotencia);
                if (await _unitOfWork.CommitAsync())
                    return new ResponseDto(true, id);
            }
            return new ResponseDto(false, MessagesResource.APPLICATION_ERROR);
        }
    }
}
