using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;
using System.Drawing;

namespace Noted.Repositories
{
    public class ChecklistItemsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ChecklistItemsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChecklistItem> AddAsync(ChecklistItemAddVM checklistItemVM, CancellationToken cancellationToken = default)
        {
            var checklistItem = new ChecklistItem()
            {
                Description = checklistItemVM.Description,
                ChecklistId = checklistItemVM.ChecklistId,
                Checked = false
            };
            await _dbContext.ChecklistItems.AddAsync(checklistItem, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return checklistItem;
        }
        public async Task<ChecklistItem?> EditAsync(ChecklistItemEditVM obj, CancellationToken cancellationToken = default)
        {
            var checklistItem = await _dbContext.ChecklistItems.FindAsync(new object?[] { obj.Id }, cancellationToken: cancellationToken);
            if (checklistItem != null)
            {
                checklistItem.Description = obj.Description;
                checklistItem.Checked = obj.Checked;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return checklistItem;
        }

        public async Task DeleteAsync(int cliId, CancellationToken cancellationToken = default)
        {
            var item = await _dbContext.ChecklistItems.FindAsync(new object?[] { cliId }, cancellationToken: cancellationToken);
            if (item != null)
            {
                _dbContext.ChecklistItems.Remove(item);
            }
         
            await _dbContext.SaveChangesAsync();
        }
      
    }
}
