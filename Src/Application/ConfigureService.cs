using Application.Common.BehaviourPipes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public static class ConfigureService
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			// CQRS = Mediator
			// collection add => service provider get => DI
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			//Pipeline
			services.AddTransient(typeof(IPipelineBehavior<,>),typeof(PerformanceBehaviour<,>));
			//services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachedQueryBehaviour<,>));
		}
	}
}
