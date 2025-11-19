using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.DTOs
{
    public class PersonResponse
    {
        public string Code { get; set; } = string.Empty;

        public string FamilyCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Identification { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty; 

        public string Phone { get; set; } = string.Empty;

        public char? Relation { get; set; }

        public bool IsMain { get; set; } = false;
    }
}
