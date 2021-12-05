using System;
using Microsoft.AspNetCore.Identity;

namespace CadastroDeUsuarios.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Nome { get; set; }
    }
}
