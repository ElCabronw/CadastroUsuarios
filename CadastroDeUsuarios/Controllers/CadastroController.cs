using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeUsuarios.Enums;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Models;
using CadastroDeUsuarios.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroDeUsuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : BaseController//ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CadastroController> _logger;
        private readonly ICadastroRepository _cadastroRepository;
        private readonly IAcessosRepository _acessosRepository;

        public CadastroController(ILogger<CadastroController> logger,
              UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICadastroRepository cadastroRepository,
            IAcessosRepository acessosRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _cadastroRepository = cadastroRepository;
            _acessosRepository = acessosRepository;
        }


 
        // GET api/values/5
        [HttpGet]
        [Authorize]
        [Route("obter-por-nome")]
        public IActionResult Get(string nome)
        {

            var test = User.Identity.Name;
            var usuario = _cadastroRepository.Buscar(x => x.Nome.Equals(nome)).Select(x => new
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Created = x.Created.ToString("dd/MM/yyyy hh:mm"),
                LastLogin = x.LastLogin.ToString("dd/MM/yyyy hh:mm")


            }).FirstOrDefault();

            if (usuario == null)
            {
                return Response_BadRequest("Não foi encontrado um usuário com o Id informado.");
            }

            return Ok(usuario);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var usuario =  _cadastroRepository.ObterPorId(id);
            //        .Select(x => new
            //   {
            //      Id = x.Id,
            //     Nome = x.Nome,
            //    Email = x.Email,
            //   Created = x.Created.ToString("dd/MM/yyyy"),
            //  LastLogin = x.LastLogin.ToString("dd/MM/yyyy")


            //            }).FirstOrDefault();

            if (usuario == null)
            {
                return Response_BadRequest("Não foi encontrado um usuário com o Id informado.");
            }
            var usuarioObj = new
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Created = usuario.Created.ToString("dd/MM/yyyy hh:mm"),
                LastLogin = usuario.LastLogin.ToString("dd/MM/yyyy hh:mm")

            };

            return Ok(usuarioObj);
            
        }

        [HttpGet]
        [Route("listar-usuarios")]
        public IActionResult GetAllUsers()
        {
            var query = _cadastroRepository.ObterTodosUsuarios().Select(x => new
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Created = x.Created.ToString("dd/MM/yyyy hh:mm"),
                LastLogin = x.LastLogin.ToString("dd/MM/yyyy hh:mm")


            }).ToList();


            return Ok(query);
        }


        [HttpGet]
        [Route("listar-perfis")]
        public async Task<IActionResult> GetPerfisAsync()
        {
            var consulta = await _acessosRepository.GetAcessosAsync();
            return Ok(consulta);

        
        }



        // POST api/values
        [HttpPost]
        [Route("cria-usuario")]
        public async Task<IActionResult> CriarUsuarioAsync([FromBody] NovoUsuarioViewModel novoUsuario)
        {
            if (ModelState.IsValid)
            {

                var checkEmail = await _userManager.FindByEmailAsync(novoUsuario.Email);
                if (checkEmail != null)
                {

                    return Response_BadRequest("Já existe um email associado a esse perfil.");
                }



                var user = new ApplicationUser
                {
                  UserName = novoUsuario.Email,
                  Email = novoUsuario.Email,
                  Created = DateTime.Now,
                  LastLogin = DateTime.Now,
                  Nome = novoUsuario.Name

                };
                var result = await _userManager.CreateAsync(user, novoUsuario.Password);
                if (result.Succeeded)
                {
                    if (novoUsuario.ListaDeAcessosId.Any())
                    {


                        var associarPerfil = await _cadastroRepository.AssociarPerfil(user, novoUsuario.ListaDeAcessosId);
                        if (associarPerfil)
                        {

                            return Ok("Usuario criado com sucesso.");

                        }
                        else
                        {
                            
                            return Response_BadRequest("Ocorreu um erro ao associar o perfil.");
                        }


                    }
                    else
                    {
                        return Ok("Usuario criado com sucesso.");
                    }
                }
                else
                {
                  

                    return Response_BadRequest("Erro ao criar usuário.");
                }
               
            }
            else
            {
                return Response_BadRequest("Modelo de dados incoreto.");
            }


        }


        [HttpPost]
        [Route("criar-perfil-acesso")]
        public async Task<IActionResult> CriaPerfilDeAcessoAsync([FromBody] CriarPerfilAcessoViewModel nomePerfil)
        {
            var checkExists = await _acessosRepository.GetAcessoByName(nomePerfil.NomeDoPerfil);

            if (checkExists != null)
            {
                return Response_BadRequest("Já existe um perfil criado com o mesmo nome cadastrado.");
            }
            var acessosObj = new Acessos(nomePerfil.NomeDoPerfil);
            _acessosRepository.Adicionar(acessosObj);

            return Ok("Perfil criado com sucesso.");
        }

        [HttpPost]
        [Route("associar-perfil-usuario")]
        public async Task<IActionResult> AssociarPerfilAsync([FromBody] AssociarPerfilViewModel associarPerfilViewModel )
        {
            var usuarioEF = await _userManager.FindByIdAsync(associarPerfilViewModel.IdUsuario.ToString());

            if (usuarioEF == null)
            {
              
                return Response_BadRequest("Não foi encontrado um usuário com o Id informado.");
              
            }

            var associaPerfil = await _cadastroRepository.AssociarPerfil(usuarioEF, associarPerfilViewModel.PerfisIds);
            if (!associaPerfil)
            {

                return Response_BadRequest("Ocorreu um erro ao associar o perfil para esse usuário, verifique os parâmetros de entrada.");
            }
            _cadastroRepository.OnUpdateUser(usuarioEF);
            



            return Ok("Perfis associados com sucesso.");
        }


        // PUT api/values/5
        [HttpPut]
        [Route("atualizar-usuario")]
        public async Task<IActionResult> PutAsync(Guid usuarioId, [FromBody]AtualizarUsuarioViewModel values )
        {
            var usuarioEF = await _userManager.FindByIdAsync(usuarioId.ToString());

            if (usuarioEF == null)
            {
           
                return Response_BadRequest("Não foi encontrado um usuário com o Id informado.");

            }

            if (!String.IsNullOrEmpty(values.Email))
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(usuarioEF, values.Email);

                await _userManager.ChangeEmailAsync(usuarioEF, values.Email, token);


                await _userManager.SetUserNameAsync(usuarioEF, values.Email);

            }

            if (!String.IsNullOrEmpty(values.Password))
            {
                var token =await _userManager.GeneratePasswordResetTokenAsync(usuarioEF);
                await _userManager.ResetPasswordAsync(usuarioEF, token, values.Password);

            }
         


            _cadastroRepository.OnUpdateUser(usuarioEF);

            return Ok("Usuário atualizado.");

        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("remover-usuario")]
        public async Task<IActionResult> DeleteAsync(Guid usuarioId)
        {
            var usuarioEF = await _userManager.FindByIdAsync(usuarioId.ToString());

            if (usuarioEF == null)
            {
                return Response_BadRequest("Nao foi encontrado o usuario.");
            }
            var isDeleted = await _userManager.DeleteAsync(usuarioEF);

            if (isDeleted.Succeeded)
            {
                return Ok("Usuario removido.");
            }
            else
            {
                return Response_BadRequest("Ocorreu um erro ao apagar o usuario.");
               
            }

        }
    }
}
