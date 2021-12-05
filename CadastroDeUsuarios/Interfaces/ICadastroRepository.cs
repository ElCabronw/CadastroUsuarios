using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroDeUsuarios.DTO;
using CadastroDeUsuarios.Enums;
using CadastroDeUsuarios.Models;
using CadastroDeUsuarios.ViewModels;

namespace CadastroDeUsuarios.Interfaces
{
    public interface ICadastroRepository : IRepository<ApplicationUser>
    {
        public Task<bool> AssociarPerfil(ApplicationUser usuario, List<Guid> acessos);
        Task<List<Paginas>> ObterPerfilUsuarioAsync(Guid usuarioId);
        ApplicationUser ObterPorId(Guid id);
        List<ApplicationUser> ObterTodosUsuarios();
        void OnUpdateUser(ApplicationUser usuario);
    }
}