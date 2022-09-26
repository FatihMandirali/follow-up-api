using Follow_Up.Application.Models;
using Follow_Up.Application.Models.UserDto;
using Follow_Up.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Features.Queries.UserList
{
    public class GetUserListQuery : IRequest<PaginatedList<UserFindByIdDto>>
    {
        public int Page { get; set; }
        public bool IsActive { get; set; }
    }
}
