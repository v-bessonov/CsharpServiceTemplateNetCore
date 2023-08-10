using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using CsharpServiceTemplateNetCore.DependencyInjection.Swagger;
using CsharpServiceTemplateNetCore.Models;
using Microsoft.IdentityModel.Tokens;

namespace CsharpServiceTemplateNetCore.Api.VersionNeutral;

public static class AuthApi
{
    public static WebApplication MapAuthApi
    (
        this WebApplication app
    )
    {
        app.MapPost("/token", (User user, IConfiguration configuration) =>
            {
                if (user.UserName == "vlad" && user.Password == "bessonov")
                {
                    var issuer = configuration["Jwt:Issuer"];
                    var audience = configuration["Jwt:Audience"];
                    var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                    
                    var now = DateTime.UtcNow;
                    var expiredOn = now.Add(TimeSpan.FromMinutes(30));
                    
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti,
                                Guid.NewGuid().ToString())
                        }),
                        Expires = expiredOn,
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    
                    return new JwtToken
                    {
                        Token = jwtToken,
                        ExpiredOn = expiredOn
                    };
                }

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            })
            .WithApiVersionSet(app.GetApiVersionSet())
            .IsApiVersionNeutral()
            .AllowAnonymous()
            .Produces<string>()
            .Produces(401)
            .Produces(500);

        return app;
    }
}