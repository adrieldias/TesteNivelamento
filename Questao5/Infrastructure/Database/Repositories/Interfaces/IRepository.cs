using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Database.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(string id);

        Task<string> AddAsync(T item);

        T Edit(T item);

        void Delete(T item);
    }

}
