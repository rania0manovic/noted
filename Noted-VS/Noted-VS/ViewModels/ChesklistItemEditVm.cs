using Noted.Entities;

namespace Noted.ViewModels
{
    public class ChecklistItemEditVM
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool Checked { get; set; }
    }
}