import log from "loglevel";
import { ApiClient } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for moving an assignment over another assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @since 0.0.1
 */
export const getMoveAssignmentOverMutationKey = (workspaceId: string) => [`workspaces.(${workspaceId}).moveAssignmentOver`] as const;

/**
 * Moves an assignment over another assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the IDs of the active and over assignments.
 * @param payload.activeAssignmentId - The ID of the active assignment.
 * @param payload.overAssignmentId - The ID of the assignment over which the active assignment will be moved.
 *
 * @since 0.0.1
 */
export const moveAssignmentOver = (workspaceId: string) => (payload: { activeAssignmentId: string, overAssignmentId: string }) => {
  return ApiClient.moveAssignmentOver(workspaceId, payload.activeAssignmentId, payload.overAssignmentId);
};

/**
 * Configuration for the moveAssignmentOver mutation.
 *
 * @since 0.0.1
 */
export type MoveAssignmentOverMutationConfig = MutationConfig<ReturnType<typeof moveAssignmentOver>>;

/**
 * A React hook for moving an assignment over another assignment within a workspace.
 *
 * @param {MoveAssignmentOverMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useMoveAssignmentOver = (config: MoveAssignmentOverMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getMoveAssignmentOverMutationKey(workspace.id),
    mutationFn: moveAssignmentOver(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to move Assignment(${args.activeAssignmentId}) over Assignment(${args.overAssignmentId})...`);
      config.onMutate?.(args);
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });

      log.debug(`[Workspace(${workspace.name})]: Assignment(${args.activeAssignmentId}) has been moved over Assignment(${args.overAssignmentId})`);
      config.onSuccess?.(result, args, ctx);
    }
  });
};
