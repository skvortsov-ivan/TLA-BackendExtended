namespace TLA_BackendExtended.Models
{
    public class BobContent
    {
        public int Id { get; set; }
        public string Prompt { get; set; } = "";
        public string GeneratedText { get; set; } = "";
        public DateTime CreatedAt { get; set; }

    }
}
