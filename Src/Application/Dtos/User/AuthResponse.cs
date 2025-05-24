using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
	public class AuthResponse
	{
		public string UserId { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public DateTime TokenExpiration { get; set; }
		public IList<string> Roles { get; set; }
	}
}
