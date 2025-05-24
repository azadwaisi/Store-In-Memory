using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
	public class LoginUserRequest
	{
		public required string EmailOrUserName { get; set; }
		public required string Password { get; set; }
	}
}
