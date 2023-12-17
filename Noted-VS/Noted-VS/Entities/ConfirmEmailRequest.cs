namespace Noted.Entities
{
    public class ConfirmEmailRequest
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsCompleted { get; set; }
    }
}
