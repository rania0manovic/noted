using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Noted.Common;
using Noted.Data;
using Noted.Entities;
using Noted.Helper;
using Noted.Security;
using Noted.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Noted.Repositories
{
    public class UsersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly EmailService _emailService;
        private readonly HashService _hash;
        public UsersRepository(ApplicationDbContext dbContext, EmailService emailService, HashService hash, IOptions<JwtTokenConfig> jwtTokenConfig)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _hash = hash;
            _jwtTokenConfig = jwtTokenConfig.Value;
        }

        public async Task Register([FromBody] UserVM user, CancellationToken cancellationToken = default)
        {
            User u = new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                HashedPassword = _hash.HashPassword(user.Password!)
            };
            await _dbContext.Users.AddAsync(u, cancellationToken);
            var quote = new UserQuote()
            {
                User = u,
                Text = "Be willing to be a beginner every single morning.",
                UserID = u.Id,
                ResetDate = DateTime.Now.Date.AddDays(1).AddHours(5)
            };
            await _dbContext.UserQuotes.AddAsync(quote, cancellationToken);
            var token = TokenGenerator.Generate(10);
            var request = new ConfirmEmailRequest()
            {
                UserId = u.Id,
                User = u,
                Token = token,
            };
            await _dbContext.confirmEmailRequests.AddAsync(request, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendAsync("Confirm your email", $"Hello {u.Name},<br/>" +
                $"your account was successfully created. " +
                $"To confirm your email address please click " +
                $"<a href=http://localhost:4200/confirm-email?token={token}>here</a>." +
                $"<br/><br/>Thank your for joining Noted,<br/>Noted Team", user.Email, cancellationToken: cancellationToken);
        }

        public async Task EditProfilePhotoAsync(int userId, [FromForm] PhotoAddVM photoVM, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
            if (user != null && photoVM != null && photoVM.ProfilePhoto != null)
            {
                var memoryStream = new MemoryStream();
                await photoVM.ProfilePhoto.CopyToAsync(memoryStream, cancellationToken);
                var photo = new Photo()
                {
                    ContentType = photoVM.ProfilePhoto.ContentType,
                    Data = memoryStream.ToArray(),
                };
                if (user.ProfilePhotoId == null)
                {
                    await _dbContext.Photos.AddAsync(photo, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    user.ProfilePhotoId = photo.Id;
                }
                else
                {
                    var userPhoto = await _dbContext.Photos.FirstAsync(x => x.Id == user.ProfilePhotoId, cancellationToken: cancellationToken);
                    userPhoto.ContentType = photo.ContentType;
                    userPhoto.Data = photo.Data;
                    _dbContext.Photos.Update(userPhoto);
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        }

        public async Task EditHeaderPhotoAsync(int userId, [FromForm] PhotoAddVM photoVM, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);
            if (user != null && photoVM != null && photoVM.ProfilePhoto != null)
            {
                var memoryStream = new MemoryStream();
                await photoVM.ProfilePhoto.CopyToAsync(memoryStream, cancellationToken);
                var photo = new Photo()
                {
                    ContentType = photoVM.ProfilePhoto.ContentType,
                    Data = memoryStream.ToArray(),
                };
                if (user.HeaderPhotoId == null)
                {
                    await _dbContext.Photos.AddAsync(photo, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    user.HeaderPhotoId = photo.Id;
                }
                else
                {
                    var userPhoto = _dbContext.Photos.First(x => x.Id == user.HeaderPhotoId);
                    userPhoto.ContentType = photo.ContentType;
                    userPhoto.Data = photo.Data;
                    _dbContext.Photos.Update(userPhoto);
                }
                _dbContext.SaveChanges();
            }

        }

        public async Task<SignInReponse> EditAsync([FromBody] UserVM userVM, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userVM.ID, cancellationToken: cancellationToken);
            if (user != null)
            {
                user.Name = userVM.Name;
                user.Surname = userVM.Surname;
                user.Email = userVM.Email;
                await _dbContext.SaveChangesAsync(cancellationToken);

                var secretKey = Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("FirstName", user.Name),
                    new Claim("LastName", user.Surname),
                    new Claim("Email", user.Email),

                }),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(secretKey),
                        SecurityAlgorithms.HmacSha512Signature
                    ),
                    Issuer = _jwtTokenConfig.Issuer,
                    Audience = _jwtTokenConfig.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.Duration)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var data = new SignInReponse
                {
                    Token = tokenHandler.WriteToken(token)
                };
                return data;
            }
            else return new SignInReponse() { Token = null };
        }

        public async Task<SignInReponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
            if (user == null || user.IsVerified == false)
                return new SignInReponse()
                { ErrorMessage = "User does not exist or is not verified." };
            if (!_hash.VerifyPassword(password, user.HashedPassword))
                return new SignInReponse()
                { ErrorMessage = "Wrong email or password." };
            var userVM = new UserVM()
            {
                Name = user.Name,
                Email = user.Email,
                Surname = user.Surname,
                ID = user.Id
            };
            var secretKey = Encoding.ASCII.GetBytes(_jwtTokenConfig.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("FirstName", user.Name),
                    new Claim("LastName", user.Surname),
                    new Claim("Email", user.Email),

                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha512Signature
                ),
                Issuer = _jwtTokenConfig.Issuer,
                Audience = _jwtTokenConfig.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.Duration)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var data = new SignInReponse
            {
                Token = tokenHandler.WriteToken(token)
            };
            return data;
        }

        public async Task<User?> GetById(int id, CancellationToken cancellationToken = default) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
