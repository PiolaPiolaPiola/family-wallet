namespace FamilyWallet.Infraestructure.DTOs.Email
{
    public class EmailBulkResponse
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
