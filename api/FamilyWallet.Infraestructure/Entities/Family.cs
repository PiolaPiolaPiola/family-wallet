using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.Entities
{
    public class Family
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Para que bd no genere la PK
        public string Code { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 16); //Código único corto

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CodeInvitation { get; set; } = string.Empty;

        public bool Status { get; set; } = true;
    }
}
