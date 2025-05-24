using Application.Dtos.Common;
using Application.Dtos.User;
using Application.Features.Authentication.Commands.LoginUser;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
	public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ResultDto>
	{
		private readonly UserManager<User> _userManager;
		public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
			_userManager = userManager;
        }
        public async Task<ResultDto> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId);
			if (user == null)
			{
				return new ResultDto { IsSuccess = false , Message = "Not Found User" };
			}
			var result = await _userManager.ConfirmEmailAsync(user, request.Token);

			return new ResultDto { IsSuccess = result.Succeeded, Message = string.Join(",", result.Errors.Select(x => x.Description).ToList()) };
		}
	}
}
