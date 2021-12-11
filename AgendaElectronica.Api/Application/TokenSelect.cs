using AgendaElectronica.Api.Class;
using AgendaElectronica.Api.JwtLogic;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AgendaElectronica.Api.Application
{
    public class TokenSelect
    {
        public class TokenSelectCommand : IRequest<RespuestaAutenticacion>
        {
            public CredencialUsuario CredencialUsuario { get; set; }
        }

        public class TokenSelectHandler : IRequestHandler<TokenSelectCommand, RespuestaAutenticacion>
        {
            private readonly IConfiguration _configuration;
            private readonly SignInManager<IdentityUser> _signInManager;

            public TokenSelectHandler(SignInManager<IdentityUser> signInManager, IConfiguration configuration)
            {
                _configuration = configuration;
                _signInManager = signInManager;
            }
            public async Task<RespuestaAutenticacion> Handle(TokenSelectCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _signInManager.PasswordSignInAsync(request.CredencialUsuario.Email,
                    request.CredencialUsuario.Password, isPersistent: false, lockoutOnFailure: false); 

                if (usuario.Succeeded)
                {
                    JwtGenerator jwtGenerator = new JwtGenerator(_configuration);
                    try
                    {
                        var respuestaAutenticacion = jwtGenerator.CreateToken(request.CredencialUsuario);
                        return respuestaAutenticacion;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No se pudo crear correctamente el token.");
                    }
                }
                else
                {
                    throw new Exception("No se pudo crear correctamente el token.");
                }
            }

        }
    }
}
