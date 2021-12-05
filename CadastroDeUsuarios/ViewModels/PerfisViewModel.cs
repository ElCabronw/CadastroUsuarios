using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadastroDeUsuarios.ViewModels
{
    public class PerfisViewModel
    {
     
        [DataType(DataType.Text)]
        [Display(Name = "Perfis de usuário : ")]
        [UIHint("List")]
        public List<SelectListItem> Roles { get; set; }

        public string Role { get; set; }

        public PerfisViewModel()
        {
            Roles = new List<SelectListItem>();
            Roles.Add(new SelectListItem() { Value = "1", Text = "Admin" });
            Roles.Add(new SelectListItem() { Value = "2", Text = "Operator" });
            Roles.Add(new SelectListItem() { Value = "3", Text = "User" });
        }
    }
}
