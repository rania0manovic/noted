using Noted.Entities;

namespace Noted.ViewModels
{
    public class ChecklistItemAddVM
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public int ChecklistId { get; set; }
    }
}
