using System.ComponentModel.DataAnnotations;

namespace Noted.ViewModels
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int MyProperty { get; set; }
    }
}