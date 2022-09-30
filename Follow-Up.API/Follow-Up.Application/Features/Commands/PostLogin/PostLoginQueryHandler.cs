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
    public class PostLoginQueryHandler : IRequestHandler<PostLoginQuery, LoginDto>
    {
        private readonly ITokenHelper _tokenHelper;

        public PostLoginQueryHandler(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        public Task<LoginDto> Handle(PostLoginQuery request, CancellationToken cancellationToken)
        {
            var token = _tokenHelper.CreateToken(Enums.RolesEnum.Admin, 1);
            return null;
        }
    }
}
