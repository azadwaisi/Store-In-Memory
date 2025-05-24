using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Identity
{
	public interface IJwtTokenGenerator
	{
		(string token, string refreshToken, DateTime expiration, DateTime refreshTokenExpiration) GenerateToken(User user, IList<string> roles);
	}
}
