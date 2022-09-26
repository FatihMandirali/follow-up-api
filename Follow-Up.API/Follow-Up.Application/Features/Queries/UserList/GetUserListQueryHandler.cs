using AutoMapper;
using Follow_Up.Application.Models;
using Follow_Up.Application.Models.Options;
using Follow_Up.Application.Models.UserDto;
using Follow_Up.Application.Services.Interfaces;
using Follow_Up.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Features.Queries.UserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PaginatedList<UserFindByIdDto>>
    {
        private readonly ILogger<GetUserListQueryHandler> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public GetUserListQueryHandler(ILogger<GetUserListQueryHandler> logger, IUserService userService, IMapper mapper, AppSettings appSettings)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<PaginatedList<UserFindByIdDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var query = _userService.FindBy(x => x.IsActive == request.IsActive);
            var data = await PaginatedList<User>.CreateAsync(query, request.Page, _appSettings.ListLimitOptions.Limit);
            var mapData = _mapper.Map<List<UserFindByIdDto>>(data.Items);
            var response = new PaginatedList<UserFindByIdDto>
                   (mapData, data.TotalCount, data.PageIndex, _appSettings.ListLimitOptions.Limit);
            return response;
        }
    }
}
