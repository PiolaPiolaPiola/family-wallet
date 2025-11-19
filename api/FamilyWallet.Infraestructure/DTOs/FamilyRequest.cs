using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.DTOs
{
    public class FamilyRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
