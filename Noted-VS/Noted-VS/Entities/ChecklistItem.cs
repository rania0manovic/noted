namespace Noted.Entities
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool Checked { get; set; }

        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
    }
}
