using FluentAssertions;
using static FluentAssertions.FluentActions;
using Yak.Core.Common;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.UnitTests;

public class WorkspaceTests

{
  // ------------------------------------------------------------ //
  // METHODS: Workspace
  // ------------------------------------------------------------ //

  public class WorkspaceRelatedTests
  {
    public class CreateTests
    {
      [Fact]
      public void ShouldCreateWorkspaceAndPublishEvent()
      {
        // -- Act -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Verify -- //
        workspace.Id.Should().NotBeNull();
        workspace.Id.Value.Should().NotBeEmpty();

        workspace.GetDomainEvents()
          .Should()
          .ContainSingle(e => e.GetType() == typeof(KanbanEvent.WorkspaceCreated))
          .Which.Should().BeOfType<KanbanEvent.WorkspaceCreated>()
          .And.BeEquivalentTo(
            new KanbanEvent.WorkspaceCreated(
              WorkspaceId: workspace.Id,
              WorkspaceName: workspace.Name,
              OwnerId: ownerId
            )
          );
      }
    }

    public class RenameTests
    {
      [Fact]
      public void ShouldRenameWorkspaceAndPublishAnEvent()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act -- //
        var oldName = WorkspaceName.Validate("Project").OrThrow();
        var newName = WorkspaceName.Validate("School Project").OrThrow();

        workspace.Rename(newName);

        // -- Verify -- //
        workspace.Name.Should().Be(newName);

        workspace.GetDomainEvents()
          .Should()
          .ContainSingle(e => e.GetType() == typeof(KanbanEvent.WorkspaceRenamed))
          .Which.Should().BeOfType<KanbanEvent.WorkspaceRenamed>()
          .And.BeEquivalentTo(
            new KanbanEvent.WorkspaceRenamed(
              WorkspaceId: workspace.Id,
              OldName: oldName,
              NewName: newName
            )
          );
      }
    }
  }

  // ------------------------------------------------------------ //
  // METHODS: Group management
  // ------------------------------------------------------------ //

  public class GroupManagementTests
  {
    public class CreateGroupTests
    {
      [Fact]
      public void ShouldCreateNewGroupAndPublishAnEvent()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act -- //
        var groupName = GroupName.Validate("ToDo's").OrThrow();
        var @group = workspace.CreateGroup(groupName);

        // -- Verify -- //
        @group.Name.Should().BeEquivalentTo(groupName);
        workspace.GetDomainEvents()
          .Should()
          .ContainSingle(e => e.GetType() == typeof(KanbanEvent.GroupCreated))
          .Which.Should().BeOfType<KanbanEvent.GroupCreated>()
          .And.BeEquivalentTo(
            new KanbanEvent.GroupCreated(
              WorkspaceId: workspace.Id,
              GroupId: @group.Id,
              GroupName: @group.Name
            )
          );
      }


      [Fact]
      public void ShouldCreateGroupsWithCorrectOrder()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act -- //
        var groupA = workspace.CreateGroup(
          GroupName.Validate("Group 1").OrThrow()
        );

        var groupB = workspace.CreateGroup(
          GroupName.Validate("Group 2").OrThrow()
        );

        var groupC = workspace.CreateGroup(
          GroupName.Validate("Group 3").OrThrow()
        );

        // -- Verify -- //
        groupA.Position.Should().Be(0);
        groupB.Position.Should().Be(1);
        groupC.Position.Should().Be(2);
      }
    }

    public class RenameGroupTests
    {
      [Fact]
      public void ShouldThrowAnErrorWhenGroupNameIsNotUnique()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act & Verify -- //
        var groupName = GroupName.Validate("ToDo's").OrThrow();
        workspace.CreateGroup(groupName);

        Invoking(() => workspace.CreateGroup(groupName));
      }

      [Fact]
      public void ShouldRenameGroupAndPublishAnEvent()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDo's").OrThrow()
        );

        // -- Act -- //
        var oldName = group.Name;
        var newName = GroupName.Validate("Archived").OrThrow();

        workspace.RenameGroup(group.Id, newName);

        // -- Verify -- //
        @group.Name.Should().BeEquivalentTo(newName);
        workspace.GetDomainEvents()
          .Should()
          .ContainSingle(e => e.GetType() == typeof(KanbanEvent.GroupRenamed))
          .Which.Should().BeOfType<KanbanEvent.GroupRenamed>()
          .And.BeEquivalentTo(
            new KanbanEvent.GroupRenamed(
              WorkspaceId: workspace.Id,
              GroupId: @group.Id,
              OldName: oldName,
              NewName: newName
            )
          );
      }

      [Fact]
      public void ShouldThrowAnExceptionWhenGroupNameIsNotUnique()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act & Verify -- //
        var name = GroupName.Validate("Archived").OrThrow();
        workspace.CreateGroup(name);

        Invoking(() => workspace.CreateGroup(name))
          .Should()
          .ThrowExactly<KanbanError.DuplicateGroup>();
      }

      [Fact]
      public void ShouldThrowAnExceptionWhenGroupDoesNotExist()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        // -- Act & Verify -- //
        var id = GroupId.Random();
        var name = GroupName.Validate("Archived").OrThrow();

        Invoking(() => workspace.RenameGroup(id, name)).Should().ThrowExactly<KanbanError.GroupNotFoundById>();
      }
    }

    public class DeleteGroupTests
    {
      [Fact]
      public void ShouldDeleteGroupAndPublishAnEvent()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );
        var name = GroupName.Validate("ToDo's").OrThrow();
        var group = workspace.CreateGroup(name);

        // -- Act -- //
        workspace.DeleteGroup(group.Id);

        // -- Assert -- //
        workspace.GetDomainEvents()
          .Should().ContainSingle(e => e.GetType() == typeof(KanbanEvent.GroupDeleted))
          .Which.Should().BeOfType<KanbanEvent.GroupDeleted>();
      }

      [Fact]
      public void ShouldThrowAnErrorWhenGroupDoesNotExist()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );
        // -- Act & Assert -- //
        var nonExistingGroupId = GroupId.Random();

        Invoking(() => workspace.DeleteGroup(nonExistingGroupId))
          .Should()
          .ThrowExactly<KanbanError.GroupNotFoundById>(
            "We have deleted the group earlier. It should not exist, thus leading to an error.");
      }
    }


    public class MoveGroupOverTests
    {
      [Fact]
      public void ShouldBeAbleToMoveGroupBOverGroupE()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var a = workspace.CreateGroup(GroupName.Validate("A").OrThrow());
        var b = workspace.CreateGroup(GroupName.Validate("B").OrThrow());
        var c = workspace.CreateGroup(GroupName.Validate("C").OrThrow());
        var d = workspace.CreateGroup(GroupName.Validate("D").OrThrow());
        var e = workspace.CreateGroup(GroupName.Validate("E").OrThrow());

        // -- Act -- //
        workspace.MoveGroupOver(b, e);

        // -- Verify -- //
        workspace.Groups.Should().Equal(a, c, d, e, b);
      }

      [Fact]
      public void ShouldBeAbleToMoveGroupEOverGroupC()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var a = workspace.CreateGroup(GroupName.Validate("A").OrThrow());
        var b = workspace.CreateGroup(GroupName.Validate("B").OrThrow());
        var c = workspace.CreateGroup(GroupName.Validate("C").OrThrow());
        var d = workspace.CreateGroup(GroupName.Validate("D").OrThrow());
        var e = workspace.CreateGroup(GroupName.Validate("E").OrThrow());

        // -- Act -- //
        workspace.MoveGroupOver(e, c);

        // -- Verify -- //
        workspace.Groups.Should().Equal(a, b, e, c, d);
      }

      [Fact]
      public void ShouldBeAbleToMoveGroupEOverGroupA()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var a = workspace.CreateGroup(GroupName.Validate("A").OrThrow());
        var b = workspace.CreateGroup(GroupName.Validate("B").OrThrow());
        var c = workspace.CreateGroup(GroupName.Validate("C").OrThrow());
        var d = workspace.CreateGroup(GroupName.Validate("D").OrThrow());
        var e = workspace.CreateGroup(GroupName.Validate("E").OrThrow());

        // -- Act -- //
        workspace.MoveGroupOver(e, a);

        // -- Verify -- //
        workspace.Groups.Should().Equal(e, a, b, c, d);
      }
    }
  }


  // ------------------------------------------------------------ //
  // METHODS: Assignment management
  // ------------------------------------------------------------ //

  public class AssignmentManagementTests
  {
    public class GetAssignmentTests
    {
      [Fact]
      public void ShouldThrowAnExceptionWhenAssignmentDoesNotExist()
      {
        //  -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var nonExistingAssignmentId = AssignmentId.Random();

        // -- Act -- //
        Invoking(() => workspace.GetAssignment(nonExistingAssignmentId))
          .Should()
          .ThrowExactly<KanbanError.AssignmentNotFoundById>();
      }
    }


    public class UpdateAssignmentTests
    {
      [Fact]
      public void ShouldUpdateExistingAssignmentAndPublishAnEvent()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDo's").OrThrow()
        );

        var assignment = workspace.CreateAssignment(
          groupId: group.Id,
          title: AssignmentTitle.Validate("First assignment").OrThrow(),
          description: "Some Description"
        );

        // -- Act -- //
        workspace.UpdateAssignment(
          id: assignment.Id,
          title: AssignmentTitle.Validate("First assignment with updated title").OrThrow(),
          description: "This is test description"
        );

        // -- Verify -- //
        var result = workspace.GetAssignment(assignment.Id);

        result.Title.Value.Should().Be("First assignment with updated title");
        result.Description.Should().Be("This is test description");

        workspace.GetDomainEvents()
          .Should()
          .ContainSingle(e => e.GetType() == typeof(KanbanEvent.AssignmentUpdated))
          .Which.Should().BeOfType<KanbanEvent.AssignmentUpdated>()
          .And.BeEquivalentTo(
            new KanbanEvent.AssignmentUpdated(
              WorkspaceId: workspace.Id,
              GroupId: assignment.GroupId,
              AssignmentId: result.Id,
              Title: result.Title,
              Description: result.Description
            )
          );
      }

      [Fact]
      public void ShouldThrowAnExceptionWhenAssignmentDoesNotExist()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        var nonExistingAssignmentId = AssignmentId.Random();

        // -- Act & Verify -- //
        Invoking(
          () => workspace.UpdateAssignment(
            id: nonExistingAssignmentId,
            title: AssignmentTitle.Validate("First assignment with updated title").OrThrow(),
            description: "This is test description"
          )
        ).Should().ThrowExactly<KanbanError.AssignmentNotFoundById>();
      }
    }

    public class CreateAssignmentTests
    {
      [Fact]
      public void ShouldThrowAnErrorWhenGroupDoesNotExist()
      {
        //  -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );
        var nonExistingGroupId = GroupId.Random();


        // -- Act & Verify -- //
        Invoking(
          () => workspace.CreateAssignment(
            nonExistingGroupId,
            AssignmentTitle.Validate("First assignment").OrThrow()
          )
        ).Should().ThrowExactly<KanbanError.GroupNotFoundById>();
      }

      [Fact]
      public void ShouldCreateAnAssignmentAndPublishTheEvent()
      {
        //  -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );
        var group = workspace.CreateGroup(GroupName.Validate("To do").OrThrow());

        // -- Act -- //
        var assignment = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("First assignment").OrThrow()
        );

        // -- Verify //
        var result = workspace.GetAssignment(assignment.Id);
        result.Title.Value.Should().Be("First assignment");
        result.Description.Should().BeNull();

        workspace.GetDomainEvents()
          .Should().ContainSingle(e => e.GetType() == typeof(KanbanEvent.AssignmentCreated))
          .Which.Should().BeOfType<KanbanEvent.AssignmentCreated>()
          .And.BeEquivalentTo(
            new KanbanEvent.AssignmentCreated(
              WorkspaceId: workspace.Id,
              GroupId: @group.Id,
              AssignmentId: assignment.Id,
              Title: AssignmentTitle.Validate("First assignment").OrThrow(),
              Description: null
            )
          );
      }

      [Fact]
      public void CreatedAssignmentsMustHaveCorrectOrdering()
      {
        //  -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var groupA = workspace.CreateGroup(GroupName.Validate("Group A").OrThrow());
        var groupB = workspace.CreateGroup(GroupName.Validate("Group B").OrThrow());


        //  -- Act -- //
        var assignmentAa = workspace.CreateAssignment(
          groupA.Id,
          AssignmentTitle.Validate("A").OrThrow()
        );

        var assignmentAb = workspace.CreateAssignment(
          groupA.Id,
          AssignmentTitle.Validate("B").OrThrow()
        );

        var assignmentBa = workspace.CreateAssignment(
          groupB.Id,
          AssignmentTitle.Validate("A").OrThrow()
        );

        var assignmentBb = workspace.CreateAssignment(
          groupB.Id,
          AssignmentTitle.Validate("B").OrThrow()
        );

        var assignmentBc = workspace.CreateAssignment(
          groupB.Id,
          AssignmentTitle.Validate("C").OrThrow()
        );

        // -- Verify -- //
        assignmentAa.Position.Should().Be(0);
        assignmentAb.Position.Should().Be(1);
        assignmentBa.Position.Should().Be(0);
        assignmentBb.Position.Should().Be(1);
        assignmentBc.Position.Should().Be(2);
      }

      [Fact]
      public void ShouldCreateAnAssignmentAndRetrieveIt()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDo's").OrThrow()
        );

        // -- Act -- //
        var assignment = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("First task of the day").OrThrow()
        );

        // -- Verify -- //
        workspace.GetAssignment(assignment.Id);
      }
    }

    public class MoveAssignmentTests
    {
      [Fact]
      public void ShouldBeAbleToMoveAssignmentAOnPositionOfAssignmentC()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDos").OrThrow()
        );

        var a = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("A").OrThrow()
        );

        var b = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("B").OrThrow()
        );

        var c = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("C").OrThrow()
        );

        var d = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("D").OrThrow()
        );

        var e = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("E").OrThrow()
        );

        // -- Act -- //
        workspace.MoveAssignment(a, c.Position);


        // -- Assert -- //
        group.Assignments
          .Should()
          .Equal(b, c, a, d, e);

        group.Assignments
          .Select((assignment, index) => (assignment, index))
          .ToList()
          .ForEach(el => el.assignment.Position.Should().Be((uint)el.index));
      }

      [Fact]
      public void ShouldBeAbleToMoveAssignmentAOnPositionOfAssignmentE()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDo's").OrThrow()
        );

        var a = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("A").OrThrow()
        );

        var b = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("B").OrThrow()
        );

        var c = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("C").OrThrow()
        );

        var d = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("D").OrThrow()
        );

        var e = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("E").OrThrow()
        );

        // -- Act -- //
        workspace.MoveAssignment(a, e.Position);


        // -- Assert -- //
        group.Assignments
          .Should()
          .Equal(b, c, d, e, a);

        group.Assignments
          .Select((assignment, index) => (assignment, index))
          .ToList()
          .ForEach(el => el.assignment.Position.Should().Be((uint)el.index));
      }

      [Fact]
      public void ShouldBeAbleToMoveAssignmentBOnPositionOfAssignmentC()
      {
        // -- Setup -- //
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Demo workspace").OrThrow(),
          ownerId
        );

        var group = workspace.CreateGroup(
          GroupName.Validate("ToDos").OrThrow()
        );

        var a = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("A").OrThrow()
        );

        var b = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("B").OrThrow()
        );

        var c = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("C").OrThrow()
        );

        var d = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("D").OrThrow()
        );

        var e = workspace.CreateAssignment(
          group.Id,
          AssignmentTitle.Validate("E").OrThrow()
        );

        //  -- Act -- //
        workspace.MoveAssignment(b, c.Position);


        //  -- Assert -- //
        group.Assignments
          .Should()
          .Equal(a, c, b, d, e);

        group.Assignments
          .Select((assignment, index) => (assignment, index))
          .ToList()
          .ForEach(el => el.assignment.Position.Should().Be((uint)el.index));
      }
    }

    public class MoveAssignmentOverTests
    {
      [Fact]
      public void ShouldBeAbleToMoveAssignmentsWithinWorkspace()
      {
        var ownerId = UserId.Random();
        var workspace = Workspace.Create(
          WorkspaceName.Validate("Project").OrThrow(),
          ownerId
        );

        var groupA = workspace.CreateGroup(
          GroupName.Validate("Group A").OrThrow()
        );

        var groupB = workspace.CreateGroup(
          GroupName.Validate("Group B").OrThrow()
        );

        var assignmentA = workspace.CreateAssignment(
          groupA.Id,
          AssignmentTitle.Validate("Assignment A").OrThrow()
        );

        var assignmentB = workspace.CreateAssignment(
          groupB.Id,
          AssignmentTitle.Validate("Assignment B").OrThrow()
        );

        var assignmentC = workspace.CreateAssignment(
          groupB.Id,
          AssignmentTitle.Validate("Assignment C").OrThrow()
        );

        // -- Act -- //
        workspace.MoveAssignmentOver(assignmentA, assignmentC);

        // -- Assert -- //
        groupB.Assignments.Should()
          .Equal(assignmentB, assignmentA, assignmentC);
      }
    }
  }
}