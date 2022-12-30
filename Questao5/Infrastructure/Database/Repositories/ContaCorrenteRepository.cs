using Dapper;
using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Dtos;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using System.Text;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class ContaCorrenteRepository : Repository<ContaCorrente>, IContaCorrenteRepository
    {
        private readonly DataBaseContext _dbContext;
        public ContaCorrenteRepository(DataBaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<SaldoDto> ConsultarSaldo(string idContaCorrente)
        {
            var consulta = new StringBuilder();
            consulta.Append($@"
                    SELECT 
                        contacorrente.numero NumeroContaCorrente, 
                        contacorrente.nome TitularContaCorrente,
                        '{DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss")}' DataConsulta,
                        SUM(case 
                                when IFNULL(movimento.tipomovimento,'C') = 'C' then IFNULL(movimento.valor,0)
                                else -movimento.valor
                            end) Saldo
                    FROM
                        contacorrente
                    LEFT JOIN movimento on contacorrente.idcontacorrente = movimento.idcontacorrente
                    WHERE
                        contacorrente.idcontacorrente = '{idContaCorrente}'
                    GROUP BY 
                        contacorrente.numero, 
                        contacorrente.nome
                ");
            var sql = consulta.ToString();

            var resultado = await _dbContext.Database.GetDbConnection().QueryAsync<SaldoDto>(sql);
            _dbContext.Database.GetDbConnection().Close();
            return resultado.FirstOrDefault();
        }
    }
}
