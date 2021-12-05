using System;
using CadastroDeUsuarios.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeUsuarios.Areas.Data
{
    public class UsuariosContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public UsuariosContext()
        {
        }

        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseNpgsql("User Id=postgres;Password=cataguases9;Host=localhost;Port=5432;Database=CadastroDeUsuarios;Pooling=true;");
        }

        public DbSet<Acessos> Acessos { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresExtension("uuid-ossp");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
