using Follow_Up.Application.Helpers.JWT;
using Follow_Up.Application.Models.LoginDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Features.Commands.PostLogin
{
    public class PostLoginQuery : IRequest<(AccessToken,string)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
