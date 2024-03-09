using Aliquota.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> UserEntity { get; set;}
        public DbSet<ContaEntity> ContaEntity { get; set; }

        public DbSet<ProductEntity> ProductEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>(e =>
            {
                e.ToTable("Users");

                e.HasKey(u => u.Id);

                e.HasOne(u => u.Conta)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.IdConta);
            });

            builder.Entity<ContaEntity>(e =>
            {
                e.ToTable("Conta");

                e.HasKey(c => c.Id);
            });

            builder.Entity<ProductEntity>(e =>
            {
                e.ToTable("Product");

                e.HasKey(p => p.Id);

                e.HasOne(p => p.Conta)
                .WithMany()
                .HasForeignKey(p => p.IdConta);
            });
        }
    }
}
