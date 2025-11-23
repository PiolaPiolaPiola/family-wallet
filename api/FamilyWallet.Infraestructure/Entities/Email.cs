using System.ComponentModel.DataAnnotations;

namespace FamilyWallet.Infraestructure.Entities
{
    public class Email
    {
        [Key]
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string? Titulo { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        [Required]
        public string? To { get; set; }

        [Required]
        public string? CodigoFamilia { get; set; }

        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
    }
    }
