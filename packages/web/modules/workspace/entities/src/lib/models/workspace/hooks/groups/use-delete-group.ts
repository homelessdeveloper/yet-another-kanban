import { ApiClient } from "@yak/web/shared/api";
import log from "loglevel";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for deleting a group within a workspace.
 *
 * @since 0.0.1
 * @param {string} workspaceId - The ID of the workspace.
 */
export const getUseDeleteGroupMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).deleteGroup`];

/**
 * Deletes a group within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the group ID to delete.
 * @param payload.groupId - The ID of the group to delete.
 * @since 0.0.1
 */
export const deleteGroup = (workspaceId: string) => ({ groupId }: { groupId: string }) => {
  return ApiClient.deleteGroup(workspaceId, groupId);
}

/**
 * Configuration for the useDeleteGroup hook.
 *
 * @since 0.0.1
 */
export type UseDeleteGroupMutationConfig = MutationConfig<ReturnType<typeof deleteGroup>>;

/**
 * A React hook for deleting a group within a workspace.
 *
 * @param config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useDeleteGroup = (config: UseDeleteGroupMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseDeleteGroupMutationKey(workspace.id),
    mutationFn: deleteGroup(workspace.id),
    onMutate(args) {
      log.debug(`Workspace(${workspace.name}): attempting to delete group with id: '${args.groupId}'`)
      config.onMutate?.(args)
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`Workspace(${workspace.name}): group with id: '${args.groupId}' has been deleted.`)
      config.onSuccess?.(result, args, ctx);
    }
  });
};

