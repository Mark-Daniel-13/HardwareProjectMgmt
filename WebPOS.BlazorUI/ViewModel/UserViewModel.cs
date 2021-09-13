using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebPOS.Business.Users.Models;

namespace WebPOS.BlazorUI.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public static List<UserViewModel> ToModelList(List<User> users)
        {
            if (users == null) return null;
            List<UserViewModel> userVMList = new();
            return userVMList = users.Select(user => new UserViewModel
            {
                UserId = user.UserId,
                FullName = user.GetFullName(),
                Username = user.UserName,
                Role = user.UserRole?.FirstOrDefault().Name,
                Email = user.Email,
            }).ToList();
        }
    }
    public class UserManageModel
    {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Username must have atleast 6 characters long.")]
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6, ErrorMessage = "Username must have atleast 6 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public int UserRole { get; set; }
        public static UserManageModel ToModel(User user)
        {
            if (user == null) return null;
            return new UserManageModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                UserRole = user.UserRoleId,
            };
        }
        public static User ToBusinessModel(UserManageModel user)
        {
            if (user == null) return null;
            return new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Email = user.Email,
                Password = user.Password,
                UserRoleId = user.UserRole,
            };
        }
    }
}
