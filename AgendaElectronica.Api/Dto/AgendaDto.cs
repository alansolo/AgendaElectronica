using System.ComponentModel.DataAnnotations;

namespace AgendaElectronica.Api.Dto
{
    public class AgendaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo apellido paterno es requerido")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "El campo apellido materno es requerido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo genero es requerido")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "El campo telefono mobil es requerido")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "El campo email es requerido")]
        public string Email { get; set; }
    }
}
