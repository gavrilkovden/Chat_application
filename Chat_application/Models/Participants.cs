namespace Chat_application.Models
{
    public class Participants
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
