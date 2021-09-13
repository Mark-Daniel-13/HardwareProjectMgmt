using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Users.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get;set; }
        public static List<Role> ToModelList(List<Data.Users.Models.Role> roles)
        {
            if (roles == null) return null;
            List<Role> roleModelList = new();
            return roleModelList = roles.Select(role => new Role
            {
                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description,
            }).ToList();
        }
        public static Role ToModel(Data.Users.Models.Role role)
        {
            if (role == null) return null;
            return new Role
            {
                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description,
            };
        }
        public static List<Data.Users.Models.Role> ToDataModelList(List<Role> roles)
        {
            if (roles == null) return null;
            List<Data.Users.Models.Role> roleModelList = new();
            return roleModelList = roles.Select(role => new Data.Users.Models.Role
            {
                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description,
            }).ToList();
        }
    }
}
