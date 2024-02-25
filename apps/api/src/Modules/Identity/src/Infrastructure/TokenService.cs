using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Infrastructure;


/// <since>0.0.1</since>
public record TokenService(IOptions<JwtSettings> Settings) : ITokenService
{

  public string CreateToken(Principal principal)
  {
    var claims = new List<Claim>
      {
          new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
          new(ClaimTypes.NameIdentifier, principal.Id.ToString()),
          new(ClaimTypes.Name, principal.UserName.Value),
          new(ClaimTypes.Email, principal.Email.Value),
      };

    var token = new JwtSecurityToken(
        issuer: Settings.Value.Issuer,
        audience: Settings.Value.Audience,
        // TODO: move this to settings
        expires: DateTime.Now.AddDays(5),
        claims: claims,
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Value.Secret)),
            SecurityAlgorithms.HmacSha256
        )
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
