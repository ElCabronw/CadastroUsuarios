using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CadastroDeUsuarios.Areas;
using CadastroDeUsuarios.Areas.Data;
using CadastroDeUsuarios.DTO;
using CadastroDeUsuarios.Enums;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Models;
using CadastroDeUsuarios.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CadastroDeUsuarios.Repositories
{
    public class CadastroRepository : Repository<ApplicationUser>, ICadastroRepository/// ICadastroRepository
    {
        private readonly UsuariosContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAcessosRepository _acessosRepository;
        private DbSession _db;
        public CadastroRepository(UsuariosContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IConfiguration configuration, IAcessosRepository acessosRepository,DbSession db) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _acessosRepository = acessosRepository;
            _db = db;
            
        }
        public void OnUpdateUser(ApplicationUser usuario)
        {
            usuario.Modified = DateTime.Now;
            Db.SaveChanges();

        }

        public void OnLoginUser(ApplicationUser usuario)
        {
            usuario.LastLogin = DateTime.Now;
            Db.SaveChanges();

        }


        public async Task<bool> AssociarPerfil(ApplicationUser usuario, List<Guid> acessos)
        {
           // var user = new ApplicationUser { UserName = usuario.Email, Email = usuario.Email };
            //var user = new IdentityUser { UserName = usuario.Email, Email = usuario.Email };
            //var result = await _userManager.FindByIdAsync(usuario.Id.ToString());
            List<Claim> newUserClaims = new List<Claim>();

            foreach (var acesso in acessos)
            {
                var acessoObj = _acessosRepository.Buscar(x => x.Id == acesso).FirstOrDefault();
                if (acessoObj == null)
                {
                    return false;
                }
                newUserClaims.Add(new Claim(acessoObj.NivelAcesso, acesso.ToString()));
               
            }
            var associarPerfis = await _userManager.AddClaimsAsync(usuario, newUserClaims);
            if (associarPerfis.Succeeded)
            {
                return true;
            }

            return false;

        }


        public ApplicationUser ObterPorId(Guid id)
        {
            return Db.Set<ApplicationUser>().AsNoTracking().FirstOrDefault(t => t.Id.Equals(id));
        }
        public async Task<ApplicationUser> ObterPorIdDapper(Guid usuarioId)
        {



            using (var conn = _db.Connection)
            {

                string query = @$"SELECT * FROM ""AspNetUsers"" WHERE ""Id"" = '{usuarioId}'";
                //var acessos = (await conn.QueryAsync<Acessos>(sql:query)).ToList();
                try
                {
                    var user = await conn.ExecuteScalarAsync<ApplicationUser>(query);
                    return user;
                }
                catch (Exception ex)
                {
                    return null;
                }





            }

        }
        public List<ApplicationUser> ObterTodosUsuarios()
        {
            return Db.Set<ApplicationUser>().ToList();
        }


        public async Task<List<Paginas>> ObterPerfilUsuarioAsync(Guid usuarioId)
        {
            using (var conn = _db.Connection)
            {

                StringBuilder sql = new StringBuilder();

                sql.AppendLine("	SELECT ");
                sql.AppendLine(@"	a.""ClaimType"" as Nome,");
                sql.AppendLine(@"	a.""ClaimValue"" as Codigo");
                sql.AppendLine(@"FROM public.""AspNetUserClaims"" as a");
                sql.AppendLine(@$"WHERE a.""UserId"" = '{usuarioId}';");
                var acessos = await conn.QueryAsync<Paginas>(sql.ToString());
                return acessos.ToList();
            }
        }

    }
}
