using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Services
{
    public abstract class TokenJwtServices
    {
        //private IConfiguration _config;

        //public TokenJwtServices(IConfiguration config)
        //{
        //    this._config = config;
        //}

        public static string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sistema-compras-olimpiadas-validacao-autenticacao"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "ApiSGCOlimpiada",
                    audience: "ServerDino",
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(30)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
