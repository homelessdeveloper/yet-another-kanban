using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yak.Core.Abstractions;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Infrastructure;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity;

/// <summary>
/// This is just a mock that is being used as
/// an identifier of the current assembly.
/// </summary
/// <since>0.0.1</since>
internal class IdentityModule { }



public static class Extensions
{
  ///<summary>
  /// Registers services, configurations necessary for identity module to work.
  /// <summary>
  ///
  /// <remarm>
  /// Identity module presents as with functionality to manage authentication and authorization.
  /// </remark>
  ///
  /// <since>0.0.1</since>
  public static void AddIdentityModule(this IServiceCollection services)
  {
    services.AddAbstractionsModule();

    // ------- Register Core Modules  ------- //
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IdentityModule).Assembly));

    services.AddMvc()
      .AddApplicationPart(typeof(IdentityModule).Assembly)
      .AddControllersAsServices();

    // ------- Environment  ------- //
    var provider = services.BuildServiceProvider();
    var configuration = provider.GetRequiredService<IConfiguration>();


    // ------- Settings / Options ------- //
    services.AddOptions<SecurityDbSettings>()
      .Bind(configuration.GetSection("Security:Datasource"))
      .ValidateDataAnnotations()
      .ValidateOnStart();

    services.AddOptions<JwtSettings>()
      .Bind(configuration.GetSection("Security:Jwt"))
      .ValidateDataAnnotations()
      .ValidateOnStart();


    // ------- Services ------- //
    services.AddScoped<IdentityDbInitializer>();
    services.AddSingleton<ITokenService, TokenService>();
    services.AddDbContext<SecurityDbContext>(options =>
    {
      var cfg = configuration.GetSection("Security:Datasource").Get<SecurityDbSettings>()!;
      options.UseNpgsql(cfg.ConnectionString);
    });

    services.AddIdentity<SecurityUser, IdentityRole<UserId>>(options =>
    {
      options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SecurityDbContext>()
    .AddUserManager<UserManager<SecurityUser>>()
    .AddSignInManager<SignInManager<SecurityUser>>();


    services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        var cfg = configuration.GetSection("Security:Jwt").Get<JwtSettings>()!;

        options.TokenValidationParameters = new()
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = cfg.Issuer,
          ValidAudience = cfg.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg.Secret))
        };
      });

    services.AddAuthorization(options => options.DefaultPolicy =
      new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
      .RequireAuthenticatedUser()
      .Build());
  }


  /// <summary>
  /// Performs various actions like database initialization
  /// and configuring already constructed WebApplication.
  /// </summary>
  /// <since>0.0.1</since>
  public static async Task UseIdentityModule(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    app.UseAuthentication();
    app.UseAuthorization();

    var initializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
    await initializer.InitializeDatabaseAsync();
  }
}
