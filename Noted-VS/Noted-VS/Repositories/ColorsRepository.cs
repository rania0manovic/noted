using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;

namespace Noted.Repositories
{
    public class ColorsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Color>> GetAllAsync(CancellationToken cancellationToken = default) => await _dbContext.Colors.ToListAsync(cancellationToken);
    }
}
