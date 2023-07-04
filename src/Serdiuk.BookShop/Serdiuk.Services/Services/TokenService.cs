﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serdiuk.BookShop.Domain.IdentityModels;
using Serdiuk.Persistance.Data;
using Serdiuk.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Serdiuk.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration config, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
        }

        public async Task AddNewRefreshToken(string token, string userId)
        {
            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsRevoked = false,
                Token = token,
                UserId = userId,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user, IConfiguration config)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var roles = await _userManager.GetRolesAsync(user);
            var key = Encoding.ASCII.GetBytes(config.GetSection("JwtConfig:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Role, string.Join(",",roles)),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(10000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public string GenerateRefreshToken()
        {
            var numbers = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numbers);
                return Convert.ToBase64String(numbers);
            }
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstAsync(x => x.Token == token);
        }

        public void SetRevokedRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
        }
    }
}
