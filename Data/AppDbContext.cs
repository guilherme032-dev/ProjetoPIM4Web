using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoPIM4.Models;
using ProjetoPIM4Web.Models;

namespace ProjetoPIM4Web.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        public DbSet<ProductService> ProductServices { get; set; } // Adicione esta linha
        public DbSet<Call> Calls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configuração para a chave estrangeira de Users em Call
            builder.Entity<Call>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Ou .Cascade, dependendo da regra de negócio

            // Configuração para a chave estrangeira de ProductService em Call
            builder.Entity<Call>()
                .HasOne(c => c.ProductService)
                .WithMany()
                .HasForeignKey(c => c.ProductServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Ou .Cascade, dependendo da regra de negócio
        }
    }
}
