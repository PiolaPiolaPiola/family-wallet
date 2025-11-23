namespace FamilyWallet.Infraestructure.DTOs.Email
{
    public class EmailRequestDto
    {
        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public string? CodigoFamilia { get; set; }

        public List<string?> To { get; set; }
    }
}
