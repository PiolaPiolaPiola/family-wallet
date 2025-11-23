namespace FamilyWallet.Infraestructure.DTOs.Email
{
    public class EmailBulkRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public string? CodigoFamilia { get; set; }

        public List<string?> To { get; set; }
    }
}
