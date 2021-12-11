using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaElectronica.Api.Model
{
    public class Agenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required]
        [StringLength(20)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(20)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
