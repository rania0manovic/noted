using System.ComponentModel.DataAnnotations;

namespace Noted.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsVerified { get; set; }

        public Photo? ProfilePhoto { get; set; }
        public int? ProfilePhotoId { get; set; }

        public Photo? HeaderPhoto { get; set; }
        public int? HeaderPhotoId { get; set; }

    }
}