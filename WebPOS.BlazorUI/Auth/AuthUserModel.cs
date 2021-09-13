using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.Auth
{
    public class LoginUserModel
    {
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }
    }
    public class AuthUserModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }

        public static AuthUserModel ToModel(Business.Users.Models.Token token)
        {
            return new AuthUserModel()
            {
                AccessToken = token.AccessToken,
                UserName = token.UserName,
            };
        }
    }
}
