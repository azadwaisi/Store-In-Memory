using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Common
{
	[ApiController]
	[ApiVersion(1.0)]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class BaseApiController : ControllerBase
	{
		private ISender _mediator = null!;
		protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
	}
}
