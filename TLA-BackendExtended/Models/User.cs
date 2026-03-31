namespace TLA_BackendExtended.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Userame { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InternalAdminNote { get; set; } = "Hemlig data";
    }
}
