using TS.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace TS.Data.Context
{
    public class TSContext : DbContext
    {
        public TSContext(DbContextOptions<TSContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seta por default o tamanho maximo de um campo string 
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Premio> Premio { get; set; }
        public DbSet<Cartela> Cartela { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
