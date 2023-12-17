
using Microsoft.EntityFrameworkCore;
using Noted.Entities;

namespace Noted.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableRow> TableRows { get; set; }
        public DbSet<TableRowData> TableRowDatas { get; set; }
        public DbSet <Quote> Quotes { get; set; }
        public DbSet<Repository> Repository { get; set; }
        public DbSet<UserQuote> UserQuotes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ConfirmEmailRequest> confirmEmailRequests { get; set; }
        public DbSet<Color> Colors { get; set; }
        public  DbSet<Photo> Photos { get; set; }
        public  DbSet<Checklist> Checklists { get; set; }
        public  DbSet<ChecklistItem> ChecklistItems { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        internal void SaveChangesa()
        {
            throw new NotImplementedException();
        }
    }
}
