using FluentAssertions;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Yak.Core.Abstractions;
using Yak.Core.Common;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Infrastructure;
using Yak.Modules.Kanban.IntegrationTests.Infrastructure;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.IntegrationTests;

/// TODO: Improve this tests
public class WorkspaceStoreTests
{
  // ------------------------------------------------------------ //
  // Test Template
  // ------------------------------------------------------------ //

  public class TestBase : TestEnvironment
  {
    protected IDomainEventPublisher DomainEventPublisher = null!;
    protected IWorkspaceStore Workspaces = null!;


    public override async Task InitializeAsync()
    {
      await base.InitializeAsync();

      DomainEventPublisher = A.Fake<IDomainEventPublisher>();
      Workspaces = new WorkspaceStore(
        ctx: Scope.ServiceProvider.GetRequiredService<KanbanDbContext>(),
        publisher: DomainEventPublisher
      );
    }
  }

  // ------------------------------------------------------------ //
  // Tests
  // ------------------------------------------------------------ //

  public class SaveChangesTests : TestBase
  {
    [Fact]
    public async Task ShouldPersistWorkspaceAndPublishEvents()
    {
      // Setup
      var ownerId = UserId.Random();
      var workspace = Workspace.Create(
        WorkspaceName.Validate("Project").OrThrow(),
        ownerId
      );

      var groupA = workspace.CreateGroup(
        GroupName.Validate("ToDo's").OrThrow()
      );

      workspace.CreateAssignment(
        groupA.Id,
        AssignmentTitle.Validate("First task of the day").OrThrow()
      );

      // Act
      Workspaces.Add(workspace);
      await Workspaces.SaveChanges();

      // Verify
      var result = await Workspaces.Get(workspace.Id);
      result.Should().NotBeNull();
      result.OwnerId.Should().Be(ownerId);


      // TODO: Inverstigate probable solution
      // We have to cast event because proxy cannot determine which overloaded method to call.
      A.CallTo(() =>
          DomainEventPublisher.Publish((IDomainEvent)A<KanbanEvent.WorkspaceCreated>._, A<CancellationToken>._))
        .MustHaveHappenedOnceExactly()
        .Then(
          A.CallTo(() =>
              DomainEventPublisher.Publish((IDomainEvent)A<KanbanEvent.GroupCreated>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly()
        )
        .Then(
          A.CallTo(() =>
              DomainEventPublisher.Publish((IDomainEvent)A<KanbanEvent.AssignmentCreated>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly()
        );
    }
  }
}

