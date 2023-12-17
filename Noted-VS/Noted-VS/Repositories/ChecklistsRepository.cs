using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class ChecklistsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ChecklistsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Checklist> AddAsync(ChecklistAddVM checklistVM, CancellationToken cancellationToken = default)
        {
            var checklist = new Checklist()
            {
                Name = checklistVM.Name,
                RepositoryId = checklistVM.RepositoryId

            };
            await _dbContext.Checklists.AddAsync(checklist, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return checklist;
        }

        public async Task<List<ChecklistGetVM>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var checklists = await _dbContext.Checklists.Where(n => n.RepositoryId == id).ToListAsync(cancellationToken);
            var list = new List<ChecklistGetVM>();
            foreach (var checklist in checklists)
            {
                list.Add(new ChecklistGetVM()
                {
                    Id = checklist.Id,
                    Name = checklist.Name,
                });
            }
            return list;
        }

        public async Task<Checklist?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Checklists.Include(x => x.ChecklistItems).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _dbContext.Checklists.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
            if (item != null)
            {
                _dbContext.Checklists.Remove(item);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task EditColor(int id, string color, CancellationToken cancellationToken = default)
        {
            var checklist = await _dbContext.Checklists.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
            if (checklist != null)
            {
                checklist.Color = "#" + color;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
