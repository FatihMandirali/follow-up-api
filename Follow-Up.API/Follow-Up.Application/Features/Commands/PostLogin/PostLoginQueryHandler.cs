using Follow_Up.Application.Helpers.JWT;
using Follow_Up.Application.Models.LoginDto;
using Follow_Up.Application.Services.Interfaces;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Follow_Up.Application.Features.Commands.PostLogin
{
    public class PostLoginQueryHandler : IRequestHandler<PostLoginQuery, (AccessToken,string)>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;

        public PostLoginQueryHandler(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public async Task<(AccessToken,string)> Handle(PostLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindUserByEmail(request.Email);
            if (user is null)
                return (null, "generic_notfound_message");
            var verify = BC.Verify(request.Password, user.Password);
            if (!verify)
                return (null, "password_invalid");
            var token = _tokenHelper.CreateToken(Enums.RolesEnum.Admin, user.Id);
            return (token,null);
        }

    }
}
