using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Authentication
{
	public class EmailSettings
	{
        public string From { get; set; }
        public string Name { get; set; }
        public string SmtpServer { get; set; }
        public int port { get; set; }
        public bool UseSsl { get; set; }
        public bool UseAuthentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
