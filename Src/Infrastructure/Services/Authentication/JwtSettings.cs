using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Authentication
{
	public class JwtSettings
	{
		public const string SectionName = "JwtSettings";
		public required string Secret { get; set; }
		public required string Issuer { get; set; }
		public required string Audience { get; set; }
		public int ExpiryInMinutes { get; set; }
		public int RefereshTokenExpiryInHours { get; set; }

	}
}
