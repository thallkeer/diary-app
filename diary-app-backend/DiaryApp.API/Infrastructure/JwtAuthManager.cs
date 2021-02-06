using DiaryApp.API.Settings;
using DiaryApp.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiaryApp.API.Infrastructure
{
    public class JwtAuthManager
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        public IEnumerable<Claim> GetClaims(UserDto user)
        {
            return new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
            };
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                _jwtTokenConfig.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


            return accessToken;
        }

        //var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Encoding.ASCII.GetBytes(tokenConfig.Secret);
        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = new ClaimsIdentity(new Claim[]
        //    {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //    }),
        //    Issuer = tokenConfig.Issuer,
        //    Audience = tokenConfig.Audience,
        //    Expires = DateTime.UtcNow.AddMinutes(tokenConfig.AccessTokenExpiration),
        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //};
        //var token = tokenHandler.CreateToken(tokenDescriptor);
        //var tokenString = tokenHandler.WriteToken(token);
        //    return tokenString;
    }

}
