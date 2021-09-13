using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.Auth
{
    public static class Jwt
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
            List<Claim> claims = new();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyvaluePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            //ExtractRolesFromJWT is used to seperate multiple roles
            ExtractRolesFromJwt(claims, keyvaluePairs);

            claims.AddRange(keyvaluePairs.Select(kp => new Claim(kp.Key, kp.Value.ToString())));
            return claims;
        }

        private static void ExtractRolesFromJwt(List<Claim> claims,Dictionary<string,object> keyValuePairs) {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
            if (roles != null) {
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
                if (parsedRoles.Length > 1)
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                    }
                }
                else {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }
        private static byte[] ParseBase64WithoutPadding(string base64) {
            switch (base64.Length % 4) {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
