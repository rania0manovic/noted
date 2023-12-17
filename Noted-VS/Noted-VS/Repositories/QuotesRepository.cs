using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class QuotesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public QuotesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(QuoteAddVM quote, CancellationToken cancellationToken = default)
        {
            Quote quoteObject = new Quote
            {
                Text = quote.Text
            };
            await _dbContext.Quotes.AddAsync(quoteObject, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Quote?> GetRandomAsync(CancellationToken cancellationToken = default)
        {
            Quote? quote = null;
            do
            {
                Random r = new Random();
                var id = r.Next(1, 10);
                quote = await _dbContext.Quotes.FirstOrDefaultAsync(q => q.Id == id, cancellationToken: cancellationToken);
            } while (quote == null);
            return quote;
        }

        public async Task<UserQuote> AddDailyAsync(UserQuoteAddVM quoteVM, CancellationToken cancellationToken = default)
        {
            UserQuote? userQuote = await _dbContext.UserQuotes.FirstOrDefaultAsync(x => x.UserID == quoteVM.UserID, cancellationToken: cancellationToken);
            if (userQuote == null)
            {
                userQuote = new UserQuote();
                await _dbContext.UserQuotes.AddAsync(userQuote, cancellationToken);
            }
            userQuote.UserID = quoteVM.UserID;
            userQuote.Text = quoteVM.Text;
            userQuote.ResetDate = quoteVM.ResetDate;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return userQuote;
        }

        public async Task<UserQuote> GetByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            UserQuote userQuote = await _dbContext.UserQuotes.FirstAsync(x => x.UserID == id, cancellationToken: cancellationToken);
            return userQuote;
        }

        public async Task<UserQuote?> UpdateForUserAsync([FromBody] UserQuoteAddVM userVM, CancellationToken cancellationToken = default)
        {
            UserQuote userQuote = await _dbContext.UserQuotes.FirstAsync(x => x.UserID == userVM.UserID, cancellationToken: cancellationToken);
            if (userQuote != null)
            {
                userQuote.Text = userVM.Text;
                userQuote.ResetDate = userVM.ResetDate;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            return userQuote;
        }
    }
}
