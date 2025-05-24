using Application.Contracts.Identity;
using Application.Dtos.User;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Commands.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse?>
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

		public LoginUserCommandHandler(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			IJwtTokenGenerator jwtTokenGenerator)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.LoginDetails.EmailOrUserName);
			if (user == null)
			{
				user = await _userManager.FindByNameAsync(request.LoginDetails.EmailOrUserName);
			}

			if (user == null)
			{
				// کاربر یافت نشد
				return null;
			}

			// lockoutOnFailure: false یعنی اگر رمز چند بار اشتباه زده شد، اکانت قفل نشود.
			// می‌توانید این را true کنید و تنظیمات Lockout را در Identity پیکربندی کنید.
			var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDetails.Password, lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				// رمز عبور نامعتبر
				return null;
			}

			var roles = await _userManager.GetRolesAsync(user);
			var (token,refereshToken, expiration, refereshTokenExp) = _jwtTokenGenerator.GenerateToken(user, roles);

			return new AuthResponse
			{
				UserId = user.Id,
				UserName = user.UserName ?? string.Empty,
				Email = user.Email ?? string.Empty,
				Token = token,
				TokenExpiration = expiration,
				Roles = roles
			};
		}
	}
}
