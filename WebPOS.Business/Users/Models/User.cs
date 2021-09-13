using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Business.Users.Models;

namespace WebPOS.Business.Users.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int UserRoleId { get; set; }
        public List<Role> UserRole { get; set; }
        public string GetFullName() { return string.Format("{0} {1}", this.FirstName, this.LastName); }

        public static List<User> ToModelList(List<Data.Users.Models.User> users) {
            if (users == null) return null;
            List<User> userModelList = new();
            return userModelList = users.Select(user => new User { 
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeletedAt = user.DeletedAt,
                UserRoleId =  (int)user.UserRole?.FirstOrDefault().RoleId,
                UserRole = Role.ToModelList(user.UserRole),
            }).ToList();
        }
        public static User ToModel(Data.Users.Models.User user)
        {
            if (user == null) return null;
            return new User {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeletedAt = user.DeletedAt,
                UserRole = Role.ToModelList(user.UserRole),
                UserRoleId = (int)user.UserRole?.FirstOrDefault().RoleId,
            };
        }
        public static Data.Users.Models.User ToDataModel(User user)
        {
            if (user == null) return null;
            return new Data.Users.Models.User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = (user.Password != null) ? Helpers.Encryption.EncryptMD5(user.Password) : null,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeletedAt = user.DeletedAt,
            };
        }
    }
}
