using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroDeUsuarios.Models;

namespace CadastroDeUsuarios.ViewModels
{
    public class AssociarPerfilViewModel
    {
        [Required]
        public Guid IdUsuario { get; set; }
        [Required]
        public List<Guid> PerfisIds { get; set; }
    }
}
