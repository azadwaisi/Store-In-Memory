﻿using Application.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Commands.LoginUser
{
	public class LoginUserCommand : IRequest<AuthResponse>
	{
		public required string EmailOrUserName { get; set; }
		public required string Password { get; set; }
	}
}
