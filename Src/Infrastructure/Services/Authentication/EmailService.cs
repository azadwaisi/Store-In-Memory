using Application.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Authentication
{
	public class EmailService : IEmailService
	{
		private readonly ILogger<EmailService> _logger;
		private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger,IConfiguration configuration)
        {
			_logger = logger;
			_configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
		{
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress(_configuration["EmailSettings:Name"], _configuration["EmailSettings:From"]));
				message.To.Add(MailboxAddress.Parse(to));
				message.Subject = subject;

				var bodyPart = new TextPart(isHtml ? MimeKit.Text.TextFormat.Html : MimeKit.Text.TextFormat.Plain)
				{
					Text = body
				};

				message.Body = bodyPart;

				using var client = new MailKit.Net.Smtp.SmtpClient();
				// تنظیم اتصال SMTP
				var secureSocketOptions = Convert.ToBoolean(_configuration["EmailSettings:UseSsl"]) ?
					SecureSocketOptions.StartTls : SecureSocketOptions.None;

				await client.ConnectAsync(
					_configuration["EmailSettings:SmtpServer"],
					Convert.ToInt32(_configuration["EmailSettings:Port"]),
					secureSocketOptions);

				if ( Convert.ToBoolean(_configuration["EmailSettings:UseAuthentication"]))
				{
					await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
				}

				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "خطا در ارسال ایمیل به {Email}", to);
				throw;
			}
		}

		public async Task SendEmailConfirmationAsync(string to, string token, string userId)
		{
			var encodedToken = WebUtility.UrlEncode(token);
			var confirmationLink = $"{_configuration["BackendUrl"]}/api/v1/Auth/confirm-email?userId={userId}&token={encodedToken}";

			var subject = "تایید ایمیل";
			var body = $@"
            <h2>تایید حساب کاربری</h2>
            <p>برای تایید حساب کاربری خود، لطفا روی لینک زیر کلیک کنید:</p>
            <p><a href='{confirmationLink}'>تایید ایمیل</a></p>
            <p>اگر شما درخواست ایجاد حساب کاربری نداده‌اید، لطفا این ایمیل را نادیده بگیرید.</p>";

			await SendEmailAsync(to, subject, body, true);
		}

		public Task SendPasswordResetAsync(string to, string token, string userId)
		{
			throw new NotImplementedException();
		}
	}
}
