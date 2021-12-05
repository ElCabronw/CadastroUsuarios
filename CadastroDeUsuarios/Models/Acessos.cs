using System;
namespace CadastroDeUsuarios.Models
{
    public class Acessos
    {
        public Guid Id { get; set; }
        public string NivelAcesso { get; set; }

        public Acessos(string nivelAcesso)
        {
            NivelAcesso = nivelAcesso;
        }
        public Acessos(Guid id, string nivelAcesso )
        {
            Id = id;
            NivelAcesso = nivelAcesso;
        }
    }
}
