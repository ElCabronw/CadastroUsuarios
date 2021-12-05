using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CadastroDeUsuarios.Models;

namespace CadastroDeUsuarios.Interfaces
{
    public interface IAcessosRepository : IRepository<Acessos>
    {
        Task<List<Acessos>> GetAcessosAsync();
    }
}
