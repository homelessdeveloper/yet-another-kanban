using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Assignments.CreateAssignment;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class CreateAssignmentTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToCreateAssignmentInDifferentGroups()
  {
    // -- Act -- //
    var assignmentAId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("First Task").OrThrow(),
        Description: null
      )
    );

    var assignmentBId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsArchiveGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Archived Task").OrThrow(),
        Description: null
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));

    workspace.Groups.Should()
      .ContainSingle(g => g.Id == JonsToDosGroupId.Value)
      .Which.Assignments.Should().ContainSingle(a => a.Id == assignmentAId.Value);

    workspace.Groups.Should()
      .ContainSingle(g => g.Id == JonsArchiveGroupId.Value)
      .Which.Assignments.Should().ContainSingle(a => a.Id == assignmentBId.Value);
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseWorkspaceDoesNotExist()
  {
    // -- Act & Verify -- //
    var act = () => Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: WorkspaceId.Random(),
        GroupId: GroupId.Random(),
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Some task").OrThrow(),
        Description: null
      )
    );

    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseGroupDoesNotExists()
  {
    // -- Act & Verify -- //
    var act = () => Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: GroupId.Random(),
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Some task").OrThrow(),
        Description: null
      )
    );

    await act.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseUserDoesNotOwnWorkspace()
  {
    // -- Act -- //
    var act = () => Mediator.Send(new CreateAssignmentCommand(
      WorkspaceId: JonsWorkspaceId,
      GroupId: JonsToDosGroupId,
      Principal: Daniel,
      AssignmentTitle: AssignmentTitle.Validate("First assignment").OrThrow(),
      Description: null
    ));

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}