using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;
using System.Collections.Generic;

namespace Questao5.Infrastructure.Database
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<Idempotencia> Idempotencia { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimento>().ToTable("movimento");
            modelBuilder.Entity<Movimento>().HasKey(x => x.Id);
            modelBuilder.Entity<Movimento>().Property(x => x.Id).HasColumnName("idmovimento");
            modelBuilder.Entity<Movimento>().Property(x => x.IdContaCorrente).HasColumnName("idcontacorrente");
            modelBuilder.Entity<Movimento>().Property(x => x.DataMovimento).HasColumnName("datamovimento");
            modelBuilder.Entity<Movimento>().Property(x => x.TipoMovimento).HasColumnName("tipomovimento");
            modelBuilder.Entity<Movimento>().Property(x => x.Valor).HasColumnName("valor");
            //modelBuilder.Entity<Movimento>().Ignore(x => x.CascadeMode);
            //modelBuilder.Entity<Movimento>().Ignore(x => x.ClassLevelCascadeMode);
            //modelBuilder.Entity<Movimento>().Ignore(x => x.RuleLevelCascadeMode);

            modelBuilder.Entity<ContaCorrente>().ToTable("contacorrente");
            modelBuilder.Entity<ContaCorrente>().HasKey(x => x.Id);
            modelBuilder.Entity<ContaCorrente>().Property(x => x.Id).HasColumnName("idcontacorrente");
            modelBuilder.Entity<ContaCorrente>().Property(x => x.Numero).HasColumnName("numero");
            modelBuilder.Entity<ContaCorrente>().Property(x => x.Ativo).HasColumnName("ativo");
            modelBuilder.Entity<ContaCorrente>().Property(x => x.Nome).HasColumnName("nome");
            //modelBuilder.Entity<ContaCorrente>().Ignore(x => x.CascadeMode);
            //modelBuilder.Entity<ContaCorrente>().Ignore(x => x.ClassLevelCascadeMode);
            //modelBuilder.Entity<ContaCorrente>().Ignore(x => x.RuleLevelCascadeMode);

            modelBuilder.Entity<Idempotencia>().ToTable("idempotencia");
            modelBuilder.Entity<Idempotencia>().HasKey(x => x.Id);
            modelBuilder.Entity<Idempotencia>().Property(x => x.Id).HasColumnName("chave_idempotencia");
            modelBuilder.Entity<Idempotencia>().Property(x => x.Requisicao).HasColumnName("requisicao");
            modelBuilder.Entity<Idempotencia>().Property(x => x.Resultado).HasColumnName("resultado");
            //modelBuilder.Entity<Idempotencia>().Ignore(x => x.CascadeMode);
            //modelBuilder.Entity<Idempotencia>().Ignore(x => x.ClassLevelCascadeMode);
            //modelBuilder.Entity<Idempotencia>().Ignore(x => x.RuleLevelCascadeMode);
        }        
    }
}
