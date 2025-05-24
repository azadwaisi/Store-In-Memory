using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.BehaviourPipes
{
	public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger _logger;
		private readonly Stopwatch _timer;
        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
			_timer = new Stopwatch();
        }
        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			_timer.Start();
			var response = await next();
			_timer.Stop();
			var elapsed = _timer.ElapsedMilliseconds;
			if (elapsed <= 500) return response;
			var requestName = typeof(TRequest).Name;
			//var userId = _currenUserService.UserId;
			//var PhoneNumber = _currentUserService.PhoneNumber;
			_logger.LogWarning("Clean Archit.. Running Request: {Name} ({elapsed} miliseconds) {@UserId}", requestName, elapsed, request);

			return response;
		}
	}
}
