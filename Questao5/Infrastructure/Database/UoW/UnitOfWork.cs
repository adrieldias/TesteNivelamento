using Microsoft.EntityFrameworkCore;

namespace Questao5.Infrastructure.Database.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var modified = _context.ChangeTracker.Entries().Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);
            if (!modified.Any())
                return true;
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
