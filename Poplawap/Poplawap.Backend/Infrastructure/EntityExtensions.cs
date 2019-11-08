using Microsoft.AspNetCore.Identity;
using Poplawap.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poplawap.Backend.Infrastructure
{
    public static class EntityExtensions
    {
        public static DTO.UserDTO MapUserResponse(this ApplicationUser user) =>
            new DTO.UserDTO 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Verified = user.Verified,
                County = user.County                
            };
    }
}
