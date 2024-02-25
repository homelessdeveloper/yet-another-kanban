import log from "loglevel";
import { useWorkspace } from "../../context";
import { useMoveAssignmentOver, useMoveAssignmentTo, useMoveGroupOver } from "../";
import { DragEndEvent, DragOverEvent, DragStartEvent } from "@dnd-kit/core";
import { DraggedItem } from "../../types";

/**
 * Hook for handling drag and drop events.
 *
 * @since 0.0.1
 */
export const useEventHandlers = () => {
  const { workspace, setState } = useWorkspace();
  const moveGroupOver = useMoveGroupOver();
  const moveAssignmentOver = useMoveAssignmentOver();
  const moveAssignmentTo = useMoveAssignmentTo();

  /**
   * Handler for the drag start event.
   *
   * @param {DragStartEvent} e - The drag start event.
   * @since 0.0.1
   */
  const onDragStart = (e: DragStartEvent) => setState(state => {
    state.draggedItem = e.active.data.current as DraggedItem
  });

  /**
   * Handler for the drag over event.
   *
   * @param {DragOverEvent} e - The drag over event.
   * @since 0.0.1
   */
  const onDragOver = (e: DragOverEvent) => {
    const { active, over } = e;

    if (!active || !over || !workspace) return;

    const activeData = active.data.current as DraggedItem;
    const overData = over.data.current as DraggedItem;

    if (activeData._tag === "None" || overData._tag == "None") {
      log.debug(`[Workspace(${workspace.name})]: Both over and active data are 'None'. Do nothing.`);
      return;
    }

    if (activeData.id == overData.id) {
      log.debug(`[Workspace(${workspace.name})]: Active element and over element are the same elements. Do nothing.`);
      return;
    }

    if (activeData._tag === "Assignment" && overData._tag === "Group") {
      log.debug(`[Workspace(${workspace.name})]: Active element is an assignment and over element is a group. Move assignment to that group.`);
      moveAssignmentTo.mutate({
        assignmentId: activeData.id,
        groupId: overData.id
      });
      return;
    }

    if (activeData._tag === "Group" && overData._tag === "Group") {
      log.debug(`[Workspace(${workspace.name})]: Active element is a group and over element is a group. Move group over.`);
      moveGroupOver.mutate({
        activeGroupId: activeData.id,
        overGroupId: overData.id,
      });
    }

    if (activeData._tag === "Assignment" && overData._tag === "Assignment") {
      log.debug(`[Workspace(${workspace.name})]: Active element is an assignment and over element is an assignment. Move assignment over.`);
      moveAssignmentOver.mutate({
        activeAssignmentId: activeData.id,
        overAssignmentId: overData.id,
      });
    }
  };

  /**
   * Handler for the drag end event.
   *
   * @param {DragEndEvent} e - The drag end event.
   * @since 0.0.1
   */
  const onDragEnd = async (e: DragEndEvent) => {
    setState(state => {
      state.draggedItem = DraggedItem.None();
    });
  };

  return {
    onDragStart,
    onDragOver,
    onDragEnd
  };
};
