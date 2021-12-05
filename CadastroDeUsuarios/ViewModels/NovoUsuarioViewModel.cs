using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroDeUsuarios.Enums;
using CadastroDeUsuarios.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadastroDeUsuarios.ViewModels
{
    public class NovoUsuarioViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deve possuir entre 6 até 100 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "As senhas não batem.")]
        public string ConfirmPassword { get; set; }

        public List<Guid> ListaDeAcessosId { get; set; }
    }
}
