using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IEmailService
	{
		Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
		Task SendEmailConfirmationAsync(string to, string token, string userId);
		Task SendPasswordResetAsync(string to, string token, string userId);
	}
}
