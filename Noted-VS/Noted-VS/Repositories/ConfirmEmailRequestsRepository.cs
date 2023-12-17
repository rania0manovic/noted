using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class ConfirmEmailRequestsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UsersRepository _usersRepository;
        public ConfirmEmailRequestsRepository(ApplicationDbContext dbContext, UsersRepository usersRepository)
        {
            _dbContext = dbContext;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResult<ResultMessageVM>> IsValidAsync(string token, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.confirmEmailRequests.FirstOrDefaultAsync(x => x.Token == token && x.IsCompleted == false, cancellationToken);
            var response = new ResultMessageVM();
            if (result != null)
            {
                result.IsCompleted = true;
                var user = await _usersRepository.GetById(result.UserId, cancellationToken);
                if (user != null)
                {
                    user.IsVerified = true;
                    response.Message = "You have successfully verified your email!";
                }
                else response.Message = "There was an error while trying to find user in the database";
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
                response.Message = ("There was an error while trying to verify your email. Please try again later.");
            return response;
        }
    }
}
