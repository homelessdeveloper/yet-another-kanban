using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Assignments.CreateAssignment;
using Yak.Modules.Kanban.Features.Assignments.MoveAssignmentOver;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class MoveAssignmentsOver : TestBase
{
  [Fact]
  public async Task ShouldThrowAnErrorIfWorkspaceDoesNotExist()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: WorkspaceId.Random(),
        ActiveAssignmentId: AssignmentId.Random(),
        OverAssignmentId: AssignmentId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
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

    var assignmentBId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment B").OrThrow(),
        Description: null
      )
    );


    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentAId,
        OverAssignmentId: assignmentBId,
        Principal: Daniel
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorIfAssignmentDoesNotExist()
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
    var act1 = () => Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentAId,
        OverAssignmentId: AssignmentId.Random(),
        Principal: Jon
      )
    );

    var act2 = () => Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: AssignmentId.Random(),
        OverAssignmentId: assignmentAId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act1.Should().ThrowExactlyAsync<KanbanError.AssignmentNotFoundById>();
    await act2.Should().ThrowExactlyAsync<KanbanError.AssignmentNotFoundById>();
  }

  [Fact]
  public async Task UserShouldBeAbleToMoveAssignmentsWithinGroup()
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

    var assignmentBId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment B").OrThrow(),
        Description: null
      )
    );

    var assignmentCId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment C").OrThrow(),
        Description: null
      )
    );

    var assignmentDId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment D").OrThrow(),
        Description: null
      )
    );

    var assignmentEId = await Mediator.Send(
      new CreateAssignmentCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: JonsToDosGroupId,
        Principal: Jon,
        AssignmentTitle: AssignmentTitle.Validate("Assignment E").OrThrow(),
        Description: null
      )
    );

    // -- Act -- //
    await Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentEId,
        OverAssignmentId: assignmentAId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentAId,
        OverAssignmentId: assignmentCId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentEId,
        OverAssignmentId: assignmentDId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveAssignmentOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveAssignmentId: assignmentBId,
        OverAssignmentId: assignmentCId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));
    workspace.Groups.Should().ContainSingle(g => g.Id == JonsToDosGroupId.Value)
      .Which.Assignments
      .Select(a => a.Title)
      .ToList()
      .Should()
      .ContainInOrder(
        "Assignment C",
        "Assignment B",
        "Assignment A",
        "Assignment D",
        "Assignment E"
      );
  }
}