using AutoMapper;
using Follow_Up.Application.Models.UserDto;
using Follow_Up.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Mapping
{
    public class FollowUpProfile : Profile
    {
        public FollowUpProfile()
        {
            CreateMap<UserFindByIdDto, User>().ReverseMap();
        }
    }
}
