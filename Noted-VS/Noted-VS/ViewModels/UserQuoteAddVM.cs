using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noted.ViewModels
{
    public class UserQuoteAddVM
    {
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime ResetDate { get; set; }

    }
}