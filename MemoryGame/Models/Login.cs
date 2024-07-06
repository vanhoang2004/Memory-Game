namespace MemoryGame.Models
{
    public class Login
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public int StageId { get; set; }
        public Stage Stage { get; set; }
    }
}
