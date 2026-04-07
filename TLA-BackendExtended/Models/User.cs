namespace TLA_BackendExtended.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }

        public int Weight { get; set; }

        public string Location { get; set; } = string.Empty;

        public bool DarkMode { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
