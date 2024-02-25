import log from "loglevel";
import { ApiClient } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for deleting an assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 *
 * @since 0.0.1
 */
export const getUseDeleteAssignmentMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).deleteAssignment`];

/**
 * Deletes an assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the assignment ID to delete.
 * @param payload.assignmentId - The ID of the assignment to delete.
 *
 * @since 0.0.1
 */
export const deleteAssignment = (workspaceId: string) => (payload: { assignmentId: string }) => {
  return ApiClient.deleteAssignment(workspaceId, payload.assignmentId);
}

/**
 * Configuration for the useDeleteAssignment hook.
 *
 * @since 0.0.1
 */
export type UseDeleteAssignmentMutationConfig = MutationConfig<ReturnType<typeof deleteAssignment>>;

/**
 * A React hook for deleting an assignment within a workspace.
 *
 * @param {UseDeleteAssignmentMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useDeleteAssignment = (config: UseDeleteAssignmentMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseDeleteAssignmentMutationKey(workspace.id),
    mutationFn: deleteAssignment(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to delete assignment with id '${args.assignmentId}'... `)

      config.onMutate?.(args);
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`[Workspace(${workspace.name})]: Assignment with id '${args.assignmentId}' has been successfully deleted.`)
      config.onSuccess?.(result, args, ctx);
    }
  })
};
