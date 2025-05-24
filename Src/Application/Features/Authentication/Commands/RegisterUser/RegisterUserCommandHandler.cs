using Application.Contracts.Identity;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Commands.RegisterUser
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResult>
	{
		private readonly UserManager<User> _userManager;
		// private readonly RoleManager<Role> _roleManager; // اگر می‌خواهید نقش پیش‌فرض بدهید
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly IEmailService _emailService;
		public RegisterUserCommandHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator,IEmailService emailService/*, RoleManager<Role> roleManager*/)
		{
			_userManager = userManager;
			_jwtTokenGenerator = jwtTokenGenerator;
			_emailService = emailService;
			// _roleManager = roleManager;
		}

		public async Task<AuthResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await _userManager.FindByNameAsync(request.UserName);
			if (existingUser != null)
			{
				return new AuthResult
				{
					Success = false,
					Errors = new[] { "کاربری با این نام کاربری یا ایمیل قبلا ثبت شده است" }
				};
			}
			var user = new User
			{
				UserName = request.UserName,
				Email = request.Email,
				EmailConfirmed = false 
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				return new AuthResult
				{
					Success = false,
					Errors = result.Errors.Select(e => e.Description)
				};
			}
			// ایجاد توکن تایید ایمیل
			var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			// ارسال ایمیل تایید
			await _emailService.SendEmailConfirmationAsync(user.Email, confirmEmailToken, user.Id);

			IList<string> roles = new List<string>();
			// ایجاد JWT توکن
			var (token, refreshToken , ExpDate, refreshTokenExpire) = _jwtTokenGenerator.GenerateToken(user,roles);

			return new AuthResult
			{
				Success = true,
				Token = token,
				RefreshToken = refreshToken,
				UserId = user.Id
			};
		}
	}
}
