using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.Entities
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Para que bd no genere la PK
        public string Code { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 16); //Código único corto

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string FamilyCode { get; set; } = string.Empty;

        [Required]
        public string Identification { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty; //Controlar con expresión regular

        public string Phone { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        public char? Relation { get; set; }

        public bool IsMain { get; set; } = false;
    }
}
