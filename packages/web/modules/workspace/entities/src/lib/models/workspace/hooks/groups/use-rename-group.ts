import { ApiClient } from "@yak/web/shared/api";
import log from "loglevel";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for renaming a group within a workspace.
 *
 * @param {string} workspaceId - The ID of the workspace.
 * @since 0.0.1
 */
export const getUseRenameGroupMutationKey = (workspaceId: string) => [`workspace(${workspaceId}).renameGroup`] as const;

/**
 * Renames a group within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the group ID and the new name.
 * @param payload.groupId - The ID of the group to rename.
 * @param payload.name - The new name for the group.
 * @since 0.0.1
 */
export const renameGroup = (workspaceId: string) => (payload: { groupId: string, name: string }) => {
  return ApiClient.renameGroup(workspaceId, payload.groupId, payload);
}

/**
 * Configuration for the useRenameGroup hook.
 *
 * @since 0.0.1
 */
export type UseRenameGroupMutationConfig = MutationConfig<ReturnType<typeof renameGroup>>;

/**
 * A React hook for renaming a group within a workspace.
 *
 * @param {UseRenameGroupMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useRenameGroup = (config: UseRenameGroupMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseRenameGroupMutationKey(workspace.id),
    mutationFn: renameGroup(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempt to rename Group(${args.groupId})...`);
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`[Workspace(${workspace.name})]: Group(${args.groupId}) has been successfully renamed to '${args.name}'`)
      config.onSuccess?.(result, args, ctx);
    }
  });
};

