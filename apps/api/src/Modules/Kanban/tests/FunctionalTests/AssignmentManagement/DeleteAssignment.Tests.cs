using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Assignments.CreateAssignment;
using Yak.Modules.Kanban.Features.Assignments.DeleteAssignment;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class DeleteAssignment : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToDeleteAssignment()
  {
    // -- Setup -- //
    var assignmentId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("First Task").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    await Mediator.Send(new DeleteAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: assignmentId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));

    workspace.Groups.SelectMany(g => g.Assignments)
      .Should().NotContain(a => a.Id == assignmentId.Value);
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(new DeleteAssignmentCommand(
        WorkspaceId: WorkspaceId.Random(),
        AssignmentId: AssignmentId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenAssignmentDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(new DeleteAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: AssignmentId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.AssignmentNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var assignmentId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("First assignment").OrThrow(),
        Description: null
      )
    );


    // -- Act -- //
    var act = () => Mediator.Send(
      new DeleteAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        AssignmentId: assignmentId,
        Principal: Daniel
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}