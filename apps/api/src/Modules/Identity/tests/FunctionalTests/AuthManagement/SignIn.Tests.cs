using LanguageExt;
using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Identity.Features.SignIn;
using Yak.Modules.Identity.Features.SignUp;
using Yak.Modules.Identity.FunctionalTests.Infrastructure;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.FunctionalTests.AuthManagement;

public class SignInTests : FunctionalTestBase
{
  [Fact]
  public async Task ShouldThrowAnErrorIfEmailPasswordPairIsIncorrect()
  {
    // -- Setup -- //
    var (
      email,
      username,
      password
      ) = (
      Email.Validate("test@mail.com"),
      UserName.Validate("Jonathan"),
      Password.Validate("a1b2c3A!B@C#")
    ).Apply((a1, a2, a3) => (a1, a2, a3)).OrThrow();

    await Mediator.Send(new SignUpCommand(email, username, password));

    // -- Act-- //
    var act1 = () => Mediator.Send(
      new SignInCommand(
        Email: Email.Validate("jon@mail.com").OrThrow(),
        Password: password
      )
    );

    var act2 = () => Mediator.Send(
      new SignInCommand(
        Email: email,
        Password.Validate("abcdABCD!@#$").OrThrow()
      )
    );
    
    // -- Verify -- //
    await act1.Should().ThrowExactlyAsync<SecurityError.InvalidCredentials>();
    await act2.Should().ThrowExactlyAsync<SecurityError.InvalidCredentials>();
  }
}