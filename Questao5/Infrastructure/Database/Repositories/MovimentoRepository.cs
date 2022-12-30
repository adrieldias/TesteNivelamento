using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class MovimentoRepository : Repository<Movimento>, IMovimentoRepository
    {
        private readonly DataBaseContext _dbContext;
        public MovimentoRepository(DataBaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
