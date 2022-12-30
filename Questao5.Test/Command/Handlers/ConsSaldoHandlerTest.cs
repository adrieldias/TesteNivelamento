
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using Questao5.Infrastructure.Database.UoW;

namespace Questao5.Test.Command.Handlers
{
    public class ConsSaldoHandlerTest
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ConsSaldoHandlerTest()
        {
            _contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        }
        [Fact]
        public async Task ConsultarSaldoContaCorrenteHandler_Handle()
        {
            var query = new ConsultarSaldoContaCorrenteQuery { Id = "1" };
            var handler = new ConsultarSaldoContaCorrenteHandler(_contaCorrenteRepository);
            var result = await handler.Handle(query, new CancellationToken());
            Assert.False(result.Success);
            var conta = new ContaCorrente
            {
                Id = "1",
                Ativo = 0,
                Nome = "teste",
                Numero = 123,
            };
            _contaCorrenteRepository.GetAsync(Arg.Any<string>()).Returns<ContaCorrente>(conta);
            result = await handler.Handle(query, new CancellationToken());
            Assert.False(result.Success);
            conta.Ativo = 1;
            result = await handler.Handle(query, new CancellationToken());
            Assert.True(result.Success);
        }
    }
}