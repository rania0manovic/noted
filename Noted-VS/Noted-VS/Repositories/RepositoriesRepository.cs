using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class RepositoriesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RepositoriesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Repository> AddAsync(RepositoryAddVM repositoryVM, CancellationToken cancellationToken = default)
        {
            Repository repository = new Repository()
            {
                Name = repositoryVM.Name,
                DateCreated = DateTime.UtcNow,
                UserID = repositoryVM.UserID
            };
            await _dbContext.Repository.AddAsync(repository, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return repository;
        }

        public async Task<List<Repository>> GetAllAsync(int id, CancellationToken cancellationToken = default)
        {
            var data = await _dbContext.Repository.Where(r => r.UserID == id).OrderBy(r => r.Id).ToListAsync(cancellationToken);
            return data;
        }

        //TODO : refactor code so the id is sent in VM
        public async Task<Repository?> EditAsync(int id, RepositoryAddVM repo, CancellationToken cancellationToken = default)
        {
            var repository = await _dbContext.Repository.FirstOrDefaultAsync(r => r.Id == id);
            if (repository != null)
            {
                repository.Name = repo.Name;
                repository.DateModified = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            return repository;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            Repository? repository = await _dbContext.Repository.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
            if (repository != null)
            {
                _dbContext.Repository.Remove(repository);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
