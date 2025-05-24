using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
	public class RegisterUserRequest
	{
		public required string UserName { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public string FirstName { get; set; } // اختیاری
		public string LastName { get; set; }  // اختیاری
	}
}
