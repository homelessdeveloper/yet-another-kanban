using LanguageExt;
using FluentAssertions;
using static FluentAssertions.FluentActions;
using Yak.Core.Common;
using Yak.Modules.Identity.Features.SignIn;
using Yak.Modules.Identity.Features.SignUp;
using Yak.Modules.Identity.FunctionalTests.Infrastructure;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.FunctionalTests.AuthManagement;

public class SignUpTests : FunctionalTestBase
{
  [Fact]
  public async Task UserShouldBeAbleToRegisterAndThenAuthenticate()
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

    // -- Act & Verify -- //
    await Mediator.Send(new SignUpCommand(email, username, password));
    var principal = await Mediator.Send(new SignInCommand(email, password));

    principal.Email.Should().Be(email);
    principal.UserName.Should().Be(username);
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseUserNameIsAlreadyTaken()
  {
    // -- Act & Verify -- //
    await Mediator.Send(
      new SignUpCommand(
        Email.Validate("test@mail.com").OrThrow(),
        UserName: UserName.Validate("Jonathan").OrThrow(),
        Password.Validate("a1b2c3A!B@C#").OrThrow()
      )
    );

    await Awaiting(() =>
        Mediator.Send(
          new SignUpCommand(
            Email.Validate("test1@mail.com").OrThrow(),
            UserName: UserName.Validate("Jonathan").OrThrow(),
            Password.Validate("a1b2c3A!B@C#").OrThrow()
          )
        )
      )
      .Should()
      .ThrowExactlyAsync<SecurityError.DuplicateUserName>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseEmailAlreadyTaken()
  {
    // -- Act & Verify -- //
    await Mediator.Send(
      new SignUpCommand(
        Email.Validate("test@mail.com").OrThrow(),
        UserName: UserName.Validate("Jonathan").OrThrow(),
        Password.Validate("a1b2c3A!B@C#").OrThrow()
      )
    );

    await Awaiting(() =>
        Mediator.Send(
          new SignUpCommand(
            Email.Validate("test@mail.com").OrThrow(),
            UserName: UserName.Validate("TestUser").OrThrow(),
            Password.Validate("a1b2c3A!B@C#").OrThrow()
          )
        )
      )
      .Should()
      .ThrowExactlyAsync<SecurityError.DuplicateEmail>();
  }
}
