using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;

namespace Noted.Repositories
{
    public class PhotosRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PhotosRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Photo?> GetProfilePhotoAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Id == userId, cancellationToken);
            if (user.ProfilePhotoId == null)
                return null;
            else
            {
                var photo = await _dbContext.Photos.FindAsync(new object?[] { user.ProfilePhotoId }, cancellationToken: cancellationToken);
                return photo;
            }
        }

        public async Task<Photo?> GetHeaderPhotoAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstAsync(x => x.Id == userId, cancellationToken: cancellationToken);
            if (user.HeaderPhotoId == null)
                return null;
            else
            {
                var photo = await _dbContext.Photos.FindAsync(new object?[] { user.HeaderPhotoId }, cancellationToken: cancellationToken);
                return photo;
            }
        }
    }
}
