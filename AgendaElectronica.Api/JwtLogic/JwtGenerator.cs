using AgendaElectronica.Api.Class;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaElectronica.Api.JwtLogic
{
    public class JwtGenerator : IJwtGenerator
    {
        public IConfiguration _configuration { get; }
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RespuestaAutenticacion CreateToken(CredencialUsuario credencialUsuario)
        {
            RespuestaAutenticacion respuestaAutenticacion = new RespuestaAutenticacion();

            var claims = new List<Claim>()
            {
                new Claim("email", credencialUsuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["LlaveJwt"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            var securityToken = new JwtSecurityToken(issuer: null, audience:null, claims: claims, expires: expiration, 
                                                        signingCredentials: credential);

            respuestaAutenticacion.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            respuestaAutenticacion.Expiracion = expiration;

            return respuestaAutenticacion;
        }
    }
}
