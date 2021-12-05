using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CadastroDeUsuarios.DTO.Credentials
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "CadastroUsuarios_AvaliacaoTecnica%1999@SsiT+JWToken";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
