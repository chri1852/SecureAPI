using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel;
using System.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SecureAPI.Models;

namespace SecureAPI.Shared
{
    public class TokenOperator
    {
        private const string TokenServerKey = "4F3387EFF198DC1E4A92D5797B079275820ED463A1EF9ABBD263DB6C52DA0743490EEBFE889ADSA9B8F7C090A124A4402D2CF2FC87EFF198DC1E4A92D572AC02782FD1B870106E2330E0EDA364A";
        private JwtHeader tokenHeader;
        private JwtSecurityTokenHandler tokenHandler;

        public TokenOperator()
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenServerKey));
            tokenHeader = new JwtHeader(new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));
            tokenHandler = new JwtSecurityTokenHandler();
        }

        
        private JwtPayload CreateNewTokenPayload(User user)
        {
            return new JwtPayload
            {
                { "UserId", user.Id },
                { "AccessLevel", user.AccessLevel },
                { "Expiration", DateTime.Now.AddHours(24).ToUniversalTime().Ticks}
            };
        }

        public string GetNewTokenString(User user)
        {
            JwtSecurityToken newToken = new JwtSecurityToken(tokenHeader, CreateNewTokenPayload(user));

            return tokenHandler.WriteToken(newToken);
        }

        public User ReadToken(string token)
        {
            JwtSecurityToken tokenData;

            try
            {
                tokenData = tokenHandler.ReadJwtToken(token);
            }
            catch (ArgumentException)
            {
                throw new AuthenticationException(ExceptionMessages.TokenInvalid);
            }

            if (Int64.Parse(tokenData.Claims.First(c => c.Type == "Expiration").Value) <= DateTime.Now.ToUniversalTime().Ticks)
            {
                throw new AuthenticationException(ExceptionMessages.TokenExpired);
            }

            return new User
            {
                Id = Int32.Parse(tokenData.Claims.First(c => c.Type == "UserId").Value),
                AccessLevel = (AccessLevels)Int32.Parse(tokenData.Claims.First(c => c.Type == "AccessLevel").Value)
            };
        }
    }
}
