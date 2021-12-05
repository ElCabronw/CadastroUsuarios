using System;
using CadastroDeUsuarios.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CadastroDeUsuarios.Areas.Data
{
    public class UsuariosContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public UsuariosContext()
        {
        }
        private IConfiguration _configuration;
        public UsuariosContext(DbContextOptions<UsuariosContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connection = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connection);
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
