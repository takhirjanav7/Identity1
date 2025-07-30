using Identity.Bll.DTOs.UserDTOs;
using Identity.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Bll.Converter
{
    public static class Mappings
    {
        public static User ConvertToUser(UserCreateDTO userCreateDto)
        {
            return new User()
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                Password = userCreateDto.Password,
                UserName = userCreateDto.UserName,
            };
        }
    }
}
