using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.DTOs
{
    public class PersonRequest
    {
        [Required]
        public string FamilyCode { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Identification { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty; //Controlar con expresión regular

        public string Phone { get; set; } = string.Empty;

        public char? Relation { get; set; }
    }
}
