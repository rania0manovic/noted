using Noted.Entities;

namespace Noted.ViewModels
{
    public class TableGetFullDataVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }
        public int NumberOfColumns { get; set; }
        public string Color { get; set; }

        public TableRow TableHeaderRow { get; set; }
        public ICollection<TableRow> TableBodyRows { get; set; }
    }
}
