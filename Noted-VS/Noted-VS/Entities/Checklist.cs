namespace Noted.Entities
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
         public Repository Repository { get; set; }
        public int RepositoryId { get; set; }
        public ICollection<ChecklistItem> ChecklistItems { get; set; } = null!;
        public string Color { get; set; } = "#424686FF";

    }
}
