using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeUsuarios.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
