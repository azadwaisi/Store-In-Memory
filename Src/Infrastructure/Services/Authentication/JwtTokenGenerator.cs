using Application.Contracts.Identity;
using Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Authentication
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions)
        {
			_jwtSettings = jwtSettingsOptions.Value;
        }
        public (string token, string refreshToken, DateTime expiration , DateTime refreshTokenExpiration) GenerateToken(User user, IList<string> roles)
		{
			var claims = new List<Claim>{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id), // Subject (معمولاً User ID)
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
				new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
				new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
				// new Claim("uid", user.Id) // یک claim سفارشی برای User ID اگر ترجیح می‌دهید
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);
			var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.RefereshTokenExpiryInHours);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expiration,
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience,
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var securityToken = tokenHandler.CreateToken(tokenDescriptor);
			var token = tokenHandler.WriteToken(securityToken);
			var refreshToken = GenerateRefreshToken();

			return (token, refreshToken , expiration ,refreshTokenExpiration );
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
	}
}
