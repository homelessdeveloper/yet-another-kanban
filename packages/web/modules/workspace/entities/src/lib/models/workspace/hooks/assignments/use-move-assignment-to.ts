import log from "loglevel";
import { ApiClient } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for moving an assignment to a group within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @since 0.0.1
 */
export const getMoveAssignmentToMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).moveAssignmentTo`] as const;

/**
 * Moves an assignment to a group within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the ID of the assignment and the ID of the group.
 * @param payload.assignmentId - The ID of the assignment to move.
 * @param payload.groupId - The ID of the group to which the assignment will be moved.
 *
 * @since 0.0.1
 */
export const moveAssignmentTo = (workspaceId: string) => (payload: { assignmentId: string, groupId: string }) => {
  return ApiClient.moveAssignmentTo(workspaceId, payload.assignmentId, payload.groupId);
};

/**
 * Configuration for the moveAssignmentTo mutation.
 *
 * @since 0.0.1
 */
export type MoveAssignmentToMutationConfig = MutationConfig<ReturnType<typeof moveAssignmentTo>>;

/**
 * A React hook for moving an assignment to a group within a workspace.
 *
 * @param {MoveAssignmentToMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useMoveAssignmentTo = (config: MoveAssignmentToMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getMoveAssignmentToMutationKey(workspace.id),
    mutationFn: moveAssignmentTo(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to move Assignment(${args.assignmentId}) to Group(${args.groupId}) ...`);

      config.onMutate?.(args);
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`[Workspace(${workspace.name})]: Assignment(${args.assignmentId}) has been moved to Group(${args.groupId})`);
      config.onSuccess?.(result, args, ctx);
    }
  });
};
