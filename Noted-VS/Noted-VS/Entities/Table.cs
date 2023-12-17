using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Noted.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfColumns { get; set; }
        public string Color { get; set; } = "#EDEDEDFF";


        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }

        public ICollection<TableRow> TableRows { get; set; }

    }
}
