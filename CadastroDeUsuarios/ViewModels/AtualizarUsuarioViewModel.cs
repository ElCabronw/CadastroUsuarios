using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeUsuarios.ViewModels
{
    public class AtualizarUsuarioViewModel
    {
       

        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

      
        [StringLength(100, ErrorMessage = "A senha deve possuir entre 6 até 100 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

    }
}
