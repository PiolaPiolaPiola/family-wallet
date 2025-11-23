using System.ComponentModel.DataAnnotations;

namespace FamilyWallet.Infraestructure.Entities
{
    public class Tope
    {
        //TODO: Agregar validaciones de datos
        //TODO: Agregar relaciones con otras entidades
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Anio { get; set; }

        public int Mes { get; set; }

        public decimal ValorMaximo { get; set; }

        public decimal ValorMinimo { get; set; }

        [Required]
        public string CodigoPersona { get; set; }
    }
}