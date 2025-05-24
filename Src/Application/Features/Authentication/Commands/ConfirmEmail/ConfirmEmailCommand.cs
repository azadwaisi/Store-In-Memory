using Application.Dtos.Common;
using Domain.Entities.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
	public class ConfirmEmailCommand : IRequest<ResultDto>
	{
        public string UserId { get; set; }
		public string Token { get; set; }
    }
}
