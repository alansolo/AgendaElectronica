using AgendaElectronica.Api.Class;

namespace AgendaElectronica.Api.JwtLogic
{
    public interface IJwtGenerator
    {
        RespuestaAutenticacion CreateToken(CredencialUsuario credencialUsuario);
    }
}
