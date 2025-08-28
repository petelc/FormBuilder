using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FormBuilderAPI.Constants;
using Microsoft.IdentityModel.Tokens;

namespace ApiIntegrationTests.Helpers;

public class ApiTokenHelper
{
    public static string GetNormalUserToken()
    {
        var userName = "";
        string[] roles = { };
        
        return CreateToken(userName, roles);
    }

    private static string CreateToken(string username, string[] roles)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var key = System.Text.Encoding.UTF8.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}