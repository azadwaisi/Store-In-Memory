using Application.Dtos.User;
using Application.Features.Authentication.Commands.ConfirmEmail;
using Application.Features.Authentication.Commands.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebApi.Common;

namespace WebApi.Controllers.V1
{
	public class AuthController : BaseApiController
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserCommand request, CancellationToken cancellationToken)
		{
			return Ok(await Mediator.Send(request, cancellationToken));
		}

		[HttpGet("confirm-email")]
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand request,CancellationToken cancellationToken)
		{
			var res = await Mediator.Send(request, cancellationToken);
			if (res.IsSuccess)
			{
				string html = "<html><body><h1> حساب شما با موفقیت فعال شد. </h1></body></html>";
				return Content(html, "text/html");
			}
			else
			{
				string html = $"<html><body><h1> مشکلی پیش آمده ({res.Message}) </h1></body></html>";
				return Content(html, "text/html");
			}
		}
	}
}
