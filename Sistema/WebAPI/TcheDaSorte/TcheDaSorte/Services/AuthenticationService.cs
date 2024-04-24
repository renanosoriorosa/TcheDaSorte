using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TS.API.Extensions;
using TS.API.Interfaces;
using TS.Data.Context;
using TS.Model.Models;

namespace TS.API.Services
{
    public class AuthenticationService
    {
        private readonly TSContext _context;
        private readonly AppTokenSettings _appTokenSettings;

        public AuthenticationService(TSContext context,
            IOptions<AppTokenSettings> appTokenSettings)
        {
            _context = context;

            _appTokenSettings = appTokenSettings.Value;
        }

        public async Task<RefreshToken> GerarRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                UserName = email,
                ExpirationDate = DateTime.UtcNow.AddHours(_appTokenSettings.RefreshTokenExpiration)
            };

            _context.RefreshToken.RemoveRange(_context.RefreshToken.Where(u => u.UserName == email));
            await _context.RefreshToken.AddAsync(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken> ObterRefreshToken(Guid refreshToken)
        {
            var token = await _context.RefreshToken
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Token == refreshToken);

            return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now
                ? token
                : null;
        }
    }
}
