using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CadastroDeUsuarios.DTO;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Models;
using CadastroDeUsuarios.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using CadastroDeUsuarios.DTO.Credentials;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadastroDeUsuarios.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginController> _logger;
        private readonly ICadastroRepository _cadastroRepository;
        private readonly IAcessosRepository _acessosRepository;
      
        public LoginController(ILogger<LoginController> logger,
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




        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            Microsoft.AspNetCore.Identity.SignInResult result;
            ApplicationUser usuarioEf;
            try
            {
                usuarioEf = await _userManager.FindByEmailAsync(model.Email);
                if (usuarioEf == null)
                {
                    var errorMsg = "Usuário e / ou senha inválidos";
                    return Unauthorized(errorMsg);
                }

                result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);
            }
            catch (Exception e)
            {
                throw e;
            }

            if (result.Succeeded)
            {
                var permissoes = await GetPermissoesAsync(usuarioEf.Id);

                var response = await GerarTokenUsuarioNovo(usuarioEf.Email,
                                                              usuarioEf.Id,
                                                              usuarioEf.Nome,
                                                              permissoes
                                                            );
                return Ok(response);


            }
            else
            {
                var errorMsg = "Usuário e / ou senha inválidos";
                return Unauthorized(errorMsg);

            }
            

            return Ok();

        }
        

        private async Task<List<Paginas>> GetPermissoesAsync(Guid usuarioId)
        {
           
           var dados = await _cadastroRepository.ObterPerfilUsuarioAsync(usuarioId);

           return dados;

            
        }


        private async Task<object> GerarTokenUsuarioNovo(string email,
                                                       Guid usuarioId,
                                                       string nomeUsuario,
                                                       List<Paginas> permissoes
                                                      )
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            //  var usuario = _profissionalRepository.ObterUsuarioPorEmail(email);
            var userClaims = await _userManager.GetClaimsAsync(usuario);
            


            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.NormalizedEmail));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            userClaims.Add(new Claim("nome", (!string.IsNullOrEmpty(nomeUsuario) ? nomeUsuario.Trim() : email)));
            userClaims.Add(new Claim("userId", usuario.Id.ToString()));
            userClaims.Add(new Claim("userEmail", usuario.NormalizedEmail));
           


            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(15)
            });

            var encodedJwt = handler.WriteToken(securityToken);

            //      permissoes = this.GetPermissoes(usuarioId);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(15),
                usuario = new
                {
                    id = usuarioId,
                    claims = userClaims.Select(c => new { c.Type, c.Value }),
                    nome = !string.IsNullOrEmpty(nomeUsuario) ? nomeUsuario.Trim() : email,
                    email = email
                },
                permissoes
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
    => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    }
}
