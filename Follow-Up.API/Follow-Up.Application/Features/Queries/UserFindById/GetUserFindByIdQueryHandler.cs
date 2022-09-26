using AutoMapper;
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

namespace Follow_Up.Application.Features.Queries.UserFindById
{
    public class GetUserFindByIdQueryHandler : IRequestHandler<GetUserFindByIdQuery, UserFindByIdDto>
    {
        private readonly ILogger<GetUserFindByIdQueryHandler> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public GetUserFindByIdQueryHandler(IUserService userService, ILogger<GetUserFindByIdQueryHandler> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserFindByIdDto> Handle(GetUserFindByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindUserById(request.id);
            var map = _mapper.Map<UserFindByIdDto>(user);
            return map;
        }
    }
}
