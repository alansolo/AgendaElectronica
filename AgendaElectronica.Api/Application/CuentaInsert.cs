using AgendaElectronica.Api.Class;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AgendaElectronica.Api.Application
{
    public class CuentaInsert
    {
        public class CuentaInsertCommand : IRequest<bool>
        {
            public CredencialUsuario CredencialUsuario { get; set; }
        }

        public class CuentaInsertHandler : IRequestHandler<CuentaInsertCommand, bool>
        {
            private readonly UserManager<IdentityUser> _userManager;

            public CuentaInsertHandler(UserManager<IdentityUser> userManager, IConfiguration configuration, IMapper mapper)
            {
                _userManager = userManager;
            }
            public async Task<bool> Handle(CuentaInsertCommand request, CancellationToken cancellationToken)
            {
                var usuario = new IdentityUser { UserName = request.CredencialUsuario.Email, 
                                                    Email = request.CredencialUsuario.Email };

                var resultado = await _userManager.CreateAsync(usuario, request.CredencialUsuario.Password);

                if(resultado.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new Exception("No se pudo agregar correctamente el usuario.");
                }
            }

        }
    }
}
