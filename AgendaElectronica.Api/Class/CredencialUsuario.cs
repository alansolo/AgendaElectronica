using System.ComponentModel.DataAnnotations;

namespace AgendaElectronica.Api.Class
{
    public class CredencialUsuario
    {
        [EmailAddress]
        [Required(ErrorMessage = "El campo email es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo password es requerido")]
        public string Password { get; set; }
    }
}
