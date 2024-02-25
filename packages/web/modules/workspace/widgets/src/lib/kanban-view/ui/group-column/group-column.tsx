import * as API from '@yak/web/shared/api';
import { cn } from '@yak/libs/cn';
import { ComponentProps, FC } from 'react';
import { Sortable } from '@yak/libs/dnd-kit';
import { BiTrashAlt } from 'react-icons/bi';
import { AssignmentCard } from '../assignment-card';
import { SortableContext } from '@dnd-kit/sortable';
import { Scrollbars } from 'react-custom-scrollbars-2';
import { CreateAssignmentModal } from '@yak/web/modules/workspace/features';
import {
  DraggedItem,
  WorkspaceModel,
} from '@yak/web/modules/workspace/entities';

/**
 * Props for the GroupColumn component.
 *
 * @since 0.0.1
 */
export type GroupColumnProps = ComponentProps<'div'> & {
  group: API.GroupResponse;
};

/**
 * Component for displaying a column of assignments within a group.
 *
 * @since 0.0.1
 */
export const GroupColumn: FC<GroupColumnProps> = (props) => {
  const { group, className, ...rest } = props;
  const deleteGroup = WorkspaceModel.useDeleteGroup();

  return (
    <Sortable
      className="relative"
      options={{
        id: group.id,
        data: DraggedItem.Group(group),
      }}
    >
      {({ isDragging }) => (
        <>
          <div
            {...rest}
            className={cn(
              'absolute opacity-0',
              'flex flex-col h-full w-full',
              'bg-gray-100 border-2 border-dashed border-gray-400',
              'rounded-lg',
              isDragging && 'opacity-100',
            )}
          />

          <div
            className={cn(
              'rounded-lg',
              'relative',
              'bg-gray-50 rounded-md transition-shadow transition-colors',
              'flex flex-col flex-shrink-0 w-72 p-3',
              isDragging && 'shadow-lg bg-white  opacity-0',
              className
            )}
          >
            <div
              className={cn(
                'flex items-center justify-between  flex-shrink-0 h-10 px-2',
                isDragging && 'opacity-0'
              )}
            >
              <div className="flex items-center">
                <span className="block text-sm font-semibold">
                  {group.name}
                </span>
              </div>
              <button onClick={() => deleteGroup.mutate({ groupId: group.id })}>
                <BiTrashAlt />
              </button>
            </div>
            <div className="flex flex-col pb-2">
              <SortableContext items={group.assignments.map((a) => a.id)}>
                <Scrollbars autoHeight autoHeightMin={600} autoHide>
                  {group.assignments.map((assignment) => (
                    <AssignmentCard
                      key={assignment.id}
                      assignment={assignment}
                    />
                  ))}
                </Scrollbars>
              </SortableContext>
              <button
                onClick={() =>
                  CreateAssignmentModal.show({ groupId: group.id })
                }
                className="w-full flex items-center justify-center border-2 border-dashed  border-gray-600 p-3 rounded-md mt-4"
              >
                <div
                  className="w-5 h-5 rounded-full border-2  border-gray-600 flex items-center justify-center"
                  children="+"
                />
                <span className="ml-2 text-sm tracking-wide truncate font-medium">
                  Add assignment
                </span>
              </button>
            </div>
          </div>
        </>
      )}
    </Sortable>
  );
};
