using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Auth
{
	public class AuthResult
	{
		public bool Success { get; set; }
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public string UserId { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
