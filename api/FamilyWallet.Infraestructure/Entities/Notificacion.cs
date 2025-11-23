using System.ComponentModel.DataAnnotations;

namespace FamilyWallet.Infraestructure.Entities
{
    public class Notificacion
    {
        [Key]
        [Required]
        public string? Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        [Required]
        public string? CodigoFamilia { get; set; }

        public bool? EsExitosa { get; set; }
    }
}
