using System.ComponentModel.DataAnnotations;

namespace Noted.Entities
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
    }
}