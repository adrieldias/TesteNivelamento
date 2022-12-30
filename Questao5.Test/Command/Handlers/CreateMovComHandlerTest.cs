
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Database.UoW;

namespace Questao5.Test.Command.Handlers
{
    public class CreateMovComHandlerTest
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMovComHandlerTest()
        {
            _contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
            _movimentoRepository = Substitute.For<IMovimentoRepository>();
            _idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
        }
        [Fact]
        public async Task CreateMovimentoHandler_Handle()
        {
            var command = new CreateMovimentoCommand("1", "1", 1, "E");
            var handler = new CreateMovimentoHandler(_movimentoRepository, _idempotenciaRepository, _unitOfWork, _contaCorrenteRepository);
            var result = await handler.Handle(command, new CancellationToken());
            Assert.False(result.Success);
            var conta = new ContaCorrente
            {
                Id = "1",
                Ativo = 0,
                Nome = "teste",
                Numero = 123,
            };
            _contaCorrenteRepository.GetAsync(Arg.Any<string>()).Returns<ContaCorrente>(conta);
            result = await handler.Handle(command, new CancellationToken());
            Assert.False(result.Success);
            conta.Ativo = 1;
            result = await handler.Handle(command, new CancellationToken());
            Assert.False(result.Success);
            command.TipoMovimento = "C";
            _movimentoRepository.AddAsync(Arg.Any<Movimento>()).Returns("1");
            _unitOfWork.CommitAsync().Returns(true);
            result = await handler.Handle(command, new CancellationToken());
            Assert.True(result.Success);
            _idempotenciaRepository.GetAsync(Arg.Any<string>()).Returns<Idempotencia>(new Idempotencia
            {
                Id = "1",
                Requisicao = "1",
                Resultado = "1",
            });
            result = await handler.Handle(command, new CancellationToken());
            Assert.True(result.Success);
        }
    }
}