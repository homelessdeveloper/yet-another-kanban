import { cn } from '@yak/libs/cn';
import { Sortable } from '@yak/libs/dnd-kit';
import { DraggedItem, WorkspaceModel } from '@yak/web/modules/workspace/entities';
import { MdOutlineEditNote } from "react-icons/md"
import { BiTrashAlt } from "react-icons/bi";
import { AssignmentResponse } from '@yak/web/shared/api';
import { ContextMenu } from '@yak/web/shared/ui';
import { ComponentProps, HTMLProps, forwardRef } from 'react';
import { EditAssignmentModal } from '@yak/web/modules/workspace/features';

/**
 * Props for the AssignmentCard component.
 *
 * @since 0.0.1
 */
export type AssignmentCardProps = ComponentProps<'div'> & {
  assignment: AssignmentResponse;
} & Omit<HTMLProps<HTMLDivElement>, 'title' | 'children'>;

/**
 * Assignment card component used in the kanban view to display assignment details.
 *
 * @since 0.0.1
 */
export const AssignmentCard = forwardRef<HTMLDivElement, AssignmentCardProps>(
  (props, ref) => {
    const { assignment, className, ...rest } = props;
    const deleteAssignment = WorkspaceModel.useDeleteAssignment();

    return (
      <Sortable
        className="relative"
        options={{
          id: assignment.id,
          data: DraggedItem.Assignment(assignment)
        }}
      >
        {({ isDragging }) => (
          <>
            <div
              className={cn(
                'absolute opacity-0',
                'flex flex-col h-full w-full',
                'bg-gray-100 border-2 border-dashed border-gray-400',
                'rounded-lg',
                isDragging && 'opacity-100',
              )}
            />
            <div
              ref={ref}
              className={cn(
                'bg-white rounded-lg cursor-pointer bg-opacity-90',
                'relative flex flex-col items-start p-4 mt-3',
                'group hover:bg-opacity-100',
                className,
                isDragging && 'opacity-0',
              )}
              {...rest}
            >
              <ContextMenu as="div" className={'z-10'}>
                <ContextMenu.Button className="absolute top-0 right-0 flex items-center justify-center hidden w-5 h-5 mt-3 mr-2 text-gray-500 rounded hover:bg-gray-200 hover:text-gray-700 group-hover:flex">
                  <svg
                    className="w-4 h-4 fill-current"
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 20 20"
                    fill="currentColor"
                  >
                    <path d="M10 6a2 2 0 110-4 2 2 0 010 4zM10 12a2 2 0 110-4 2 2 0 010 4zM10 18a2 2 0 110-4 2 2 0 010 4z" />
                  </svg>
                </ContextMenu.Button>
                <ContextMenu.Items className="min-w-[160px]">
                  <>
                    <ContextMenu.Item
                      onClick={() => EditAssignmentModal.show({ assignmentId: assignment.id })}
                    >
                      <MdOutlineEditNote className="text-xl" />
                      <span>Edit assignment</span>
                    </ContextMenu.Item>

                    <ContextMenu.Divider />
                    <ContextMenu.Item
                      color="red"
                      onClick={() => deleteAssignment.mutate({ assignmentId: assignment.id, })}
                    >
                      <BiTrashAlt size={16} />
                      <span>Delete assignment</span>
                    </ContextMenu.Item>
                  </>
                </ContextMenu.Items>
              </ContextMenu>
              <h4 className="mt-3 text-sm font-medium">{assignment.title}</h4>
            </div>
          </>
        )}
      </Sortable>

    );
  }
);
