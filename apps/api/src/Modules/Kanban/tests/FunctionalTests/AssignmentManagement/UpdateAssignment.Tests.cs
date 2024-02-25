using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Assignments.CreateAssignment;
using Yak.Modules.Kanban.Features.Assignments.UpdateAssignment;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class UpdateAssignment : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToUpdateAssignment()
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
    await Mediator.Send(
      new UpdateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        Principal: Jon,
        AssignmentId: assignmentId,
        Title: AssignmentTitle.Validate("Updated Title").OrThrow(),
        Description: "Sample"
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));

    workspace.Groups.SelectMany(g => g.Assignments)
      .Should().ContainSingle(a => a.Id == assignmentId.Value)
      .Which.Should().BeEquivalentTo(new
      {
        Id = assignmentId.Value,
        Title = "Updated Title",
        Description = "Sample",
        Position = 0
      });
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseWorkspaceDoesNotExist()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new UpdateAssignmentCommand(
        WorkspaceId: WorkspaceId.Random(),
        Principal: Jon,
        AssignmentId: AssignmentId.Random(),
        Title: AssignmentTitle.Validate("Updated Title").OrThrow(),
        Description: "Sample"
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
      new UpdateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        Principal: Jon,
        AssignmentId: AssignmentId.Random(),
        Title: AssignmentTitle.Validate("Updated Title").OrThrow(),
        Description: "Sample"
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
        AssignmentTitle: AssignmentTitle.Validate("Title").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new UpdateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        Principal: Daniel,
        AssignmentId: assignmentId,
        Title: AssignmentTitle.Validate("Updated Title").OrThrow(),
        Description: "Sample"
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}