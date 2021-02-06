using DiaryApp.API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DiaryApp.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtTokenConfig jwtTokenConfig)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidateIssuer = false,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateAudience = false,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)                   
                };
            });
        }
    }
}
