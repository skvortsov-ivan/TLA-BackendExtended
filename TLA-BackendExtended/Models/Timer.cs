namespace TLA_BackendExtended.Models
{
    public class Timer
    {
        public int Id { get; set; }
        public decimal? TimeInterval { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
