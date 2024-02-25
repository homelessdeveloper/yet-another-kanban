using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Assignments.CreateAssignment;
using Yak.Modules.Kanban.Features.Assignments.MoveAssignmentTo;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class MoveAssignmentTo : TestBase
{
  [Fact]
  public async Task ShouldBeAbleToMoveAssignmentToAnotherGroup()
  {
    // -- Setup -- //
    var assignmentAId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment A").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    await Mediator.Send(
      new MoveAssignmentToCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: assignmentAId,
        GroupId: JonsArchiveGroupId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));

    workspace.Groups.Should().ContainSingle(g => g.Id == JonsToDosGroupId.Value)
      .Which.Assignments.Should().NotContain(a => a.Id == assignmentAId.Value);

    workspace.Groups.Should().ContainSingle(g => g.Id == JonsArchiveGroupId.Value)
      .Which.Assignments.Should().ContainSingle(a => a.Id == assignmentAId.Value);
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseWorkspaceDoesNotExist()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentToCommand(
        WorkspaceId: WorkspaceId.Random(),
        AssignmentId: AssignmentId.Random(),
        GroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseAssignmentDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentToCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: AssignmentId.Random(),
        GroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.AssignmentNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseGroupDoesNotExist()
  {
    // -- Setup -- //
    var assignmentAId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment A").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentToCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: assignmentAId,
        GroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
  }


  [Fact]
  public async Task ShouldThrowAnErrorIfUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var assignmentAId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment A").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentToCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: assignmentAId,
        GroupId: JonsArchiveGroupId,
        Principal: Daniel
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}