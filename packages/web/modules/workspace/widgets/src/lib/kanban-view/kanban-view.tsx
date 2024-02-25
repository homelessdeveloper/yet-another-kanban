import { createPortal } from 'react-dom';
import { SortableContext } from '@dnd-kit/sortable';
import { AssignmentCard, GroupColumn } from './ui';
import { Match, pipe } from 'effect';
import { WorkspaceModel } from '@yak/web/modules/workspace/entities';
import { CreateGroupModal } from '@yak/web/modules/workspace/features';
import {
  DndContext,
  DragOverlay,
  PointerSensor,
  useSensor,
  useSensors,
} from '@dnd-kit/core';

/**
 * Kanban view component for managing groups and assignments.
 *
 * @since 0.0.1
 */
export const KanbanView = () => {
  const {
    workspace,
    state: { draggedItem },
  } = WorkspaceModel.useWorkspace();

  const { onDragEnd, onDragStart, onDragOver } =
    WorkspaceModel.useEventHandlers();

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 12,
      },
    })
  );

  return (
    <div className="flex flex-grow px-10 mt-4 space-x-6">
      <DndContext
        sensors={sensors}
        onDragStart={onDragStart}
        onDragOver={onDragOver}
        onDragEnd={onDragEnd}
      >
        {workspace && (
          <SortableContext items={workspace.groups.map((g) => g.id)}>
            {workspace.groups.map((group) => (
              <GroupColumn key={group.id} group={group} />
            ))}
          </SortableContext>
        )}

        {createPortal(
          <DragOverlay>
            {pipe(
              Match.value(draggedItem),
              Match.tagsExhaustive({
                None: () => <></>,
                Group: (group) => (
                  <GroupColumn group={group} className="shadow-lg" />
                ),
                Assignment: (assignment) => (
                  <AssignmentCard
                    assignment={assignment}
                    className="hover:shadow-2xl"
                  />
                ),
              })
            )}
          </DragOverlay>,
          document.body
        )}
      </DndContext>

      {/* ADD NEW BUTTON */}
      <div className="flex flex-col flex-shrink-0 w-72">
        <div className="flex items-center flex-shrink-0 h-10 px-2">
          <button
            onClick={() => workspace && CreateGroupModal.show({})}
            className="w-full flex items-center justify-center border-2 border-dashed  border-gray-600 p-3 rounded-md mt-5"
          >
            <div
              className="w-5 h-5 rounded-full border-2  border-gray-600 flex items-center justify-center"
              children="+"
            />
            <span className="ml-2 text-sm tracking-wide truncate font-medium">
              Add column
            </span>
          </button>
        </div>
      </div>
      {/* ADD NEW BUTTON END */}
    </div>
  );
};
