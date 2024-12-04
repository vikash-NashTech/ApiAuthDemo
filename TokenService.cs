using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public  class TokenService
{
    
    public static string GenerateToken()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("scope","read")
            
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ghav8cc1L7CwYHnBGTMJtET99493/a1K6ElhDcn2zAc="));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            audience: "https://api.example.com",
            issuer: "https://your-oauth2-provider.com/",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
