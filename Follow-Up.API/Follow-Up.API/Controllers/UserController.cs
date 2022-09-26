using Follow_Up.Application.Enums;
using Follow_Up.Application.Features.Queries.UserFindById;
using Follow_Up.Application.Features.Queries.UserList;
using Follow_Up.Application.Models;
using Follow_Up.Application.Models.UserDto;
using LocalizationSampleSingleResxFile.Localize;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Follow_Up.API.Controllers
{
    [Route("{culture:culture}/api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Resource> _localizer;

        public UserController(ILogger<UserController> logger, IMediator mediator, IStringLocalizer<Resource> localizer)
        {
            _logger = logger;
            _mediator = mediator;
            _localizer = localizer;
        }

        /// <summary>
        /// Get User List
        /// </summary>
        /// <param name="getUserListQuery"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<BaseResponse<PaginatedList<UserFindByIdDto>>> GetUserList([FromQuery]GetUserListQuery getUserListQuery)
        {
            var response = await _mediator.Send(getUserListQuery);
            return new BaseResponse<PaginatedList<UserFindByIdDto>>(ProcessStatusEnum.Success, null, response);
        }


        /// <summary>
        /// Get UserFindById
        /// </summary>
        /// <param name="getUserFindByIdQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseResponse<UserFindByIdDto>> GetUserFindById([FromQuery] GetUserFindByIdQuery getUserFindByIdQuery)
        {
            var response = await _mediator.Send(getUserFindByIdQuery);
            if(response is not null)
                return new BaseResponse<UserFindByIdDto>(ProcessStatusEnum.Success, null, response);

            _logger.LogError($"kullanıcı bulunamadı : {getUserFindByIdQuery.id}");
            var friendly = new FriendlyMessage
            {
                Title = _localizer["generic_notfound_title"].Value,
                Message = _localizer["generic_notfound_message"].Value
            };
            return new BaseResponse<UserFindByIdDto>(ProcessStatusEnum.NotFound, friendly, response);
        }
    }
}
