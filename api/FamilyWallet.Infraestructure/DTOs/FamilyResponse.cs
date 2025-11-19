using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.DTOs
{
    public class FamilyResponse
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string CodeInvitation { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
}
