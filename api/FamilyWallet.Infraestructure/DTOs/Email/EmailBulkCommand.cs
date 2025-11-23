namespace FamilyWallet.Infraestructure.DTOs.Email
{
    public class EmailBulkCommand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public EmailRequestDto Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}