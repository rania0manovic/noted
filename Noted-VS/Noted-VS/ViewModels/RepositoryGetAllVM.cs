using System.ComponentModel.DataAnnotations;

namespace Noted.ViewModels
{
    public class RepositoryGetAllVM
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}