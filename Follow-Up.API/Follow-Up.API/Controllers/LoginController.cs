using Follow_Up.Application.Enums;
using Follow_Up.Application.Features.Commands.PostLogin;
using Follow_Up.Application.Models;
using LocalizationSampleSingleResxFile.Localize;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Follow_Up.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<Resource> _localizer;

        public LoginController(IStringLocalizer<Resource> localizer, IMediator mediator, ILogger<UserController> logger)
        {
            _localizer = localizer;
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseResponse<object>> PostLogin([FromBody] PostLoginQuery postLogin)
        {
            var response = await _mediator.Send(postLogin);
            return new BaseResponse<object>(ProcessStatusEnum.Success, null, response);
        }
    }
}
