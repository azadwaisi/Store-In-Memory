using Application.Contracts;
using Domain.Exceptions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WebApi
{
    public static class ConfigureService
	{
		public static IServiceCollection AddWebServiceCollection(this WebApplicationBuilder builder)
		{
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddControllers();
			//apiBehaviorOption
			builder.Services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState.Where(x=>x.Value.Errors.Count>0).SelectMany(x=>x.Value.Errors).Select(x=>x.ErrorMessage).ToList();
					return new BadRequestObjectResult(new ApiToReturn(400,errors));
				};
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//IHttpContext Accessor
			builder.Services.AddHttpContextAccessor();
			//cach memory
			builder.Services.AddDistributedMemoryCache();
			return builder.Services;
		}

		public static async Task<IApplicationBuilder> AddWebApplicationCollection(this WebApplication app)
		{
			var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
			//auto migration
			var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<ApplicationDbContext>();
			try
			{
				await context.Database.MigrateAsync();
				await GenerateFakeData.SeedDataAsync(context, loggerFactory);
			}
			catch (Exception e)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError($"[{DateTime.Now}][ERROR: {e.Message}]");
			}
			//
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			return app;
		}
	}
}
