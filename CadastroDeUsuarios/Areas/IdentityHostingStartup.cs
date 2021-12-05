using System;
using Microsoft.AspNetCore.Hosting;
using CadastroDeUsuarios.Areas.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Repositories;

namespace CadastroDeUsuarios.Areas
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
               // services.AddDbContext<UsuariosContext>(options =>
                //    options.UseNpgsql(
                 //       context.Configuration.GetConnectionString("DefaultConnection")));
        

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
              //      .AddEntityFrameworkStores<UsuariosContext>();



               // services.AddTransient<ICadastroRepository, CadastroRepository>();

            });
        }
    }
}
