using System.ComponentModel.DataAnnotations;

namespace Noted.ViewModels
{
    public class UsersGetAllVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

    }
}