namespace FamilyWallet.Infraestructure.DTOs.Tope
{
    public class TopeCreateDto
    {
        public int Anio { get; set; }

        public int Mes { get; set; }

        public decimal ValorMaximo { get; set; }

        public decimal ValorMinimo { get; set; }

        public string CodigoPersona { get; set; }
    }
}
