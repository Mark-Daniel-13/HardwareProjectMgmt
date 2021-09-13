using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebPOS.Business.Users;

namespace WebPOS.Business.Helpers
{
    public class JwtToken
    {
        public static async Task<Users.Models.Token> GenerateToken(int userId) {
            try
            {
                using (Users.Facades.User userDb = new())
                {
                    var user = await userDb.GetByIdWithRoles(userId);
                    if (user is null) return null;
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.GivenName,user.GetFullName()),
                    //Nbf = valid not before || Exp = expired at
                    new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
                };
                    foreach (var role in user.UserRole)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    var token = new JwtSecurityToken(
                                    new JwtHeader(
                                        new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Globals.Jwtkey)),
                                        SecurityAlgorithms.HmacSha256)),
                                new JwtPayload(claims));

                    var res = new Users.Models.Token
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        UserName = user.UserName,
                    };

                    return res;
                }
            }
            catch (Exception e) {
                var debug = e.Message;
                return null;
            }
        }
    }
}
