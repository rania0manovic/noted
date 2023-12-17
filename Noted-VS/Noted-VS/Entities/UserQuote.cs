using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noted.Entities
{
    public class UserQuote
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime ResetDate { get; set; }

    }
}