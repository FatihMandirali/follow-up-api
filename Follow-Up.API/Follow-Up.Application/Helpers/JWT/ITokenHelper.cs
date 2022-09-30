using Follow_Up.Application.Enums;
using Follow_Up.Application.Models.LoginDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Helpers.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(RolesEnum rolesEnum, int id);
    }
}
