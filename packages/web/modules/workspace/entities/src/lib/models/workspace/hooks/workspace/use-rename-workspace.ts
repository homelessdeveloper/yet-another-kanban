import z from "zod";
import { ApiClient, RenameWorkspaceRequest } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import log from "loglevel";
import { getUseGetWorkspaceByIdQueryKey } from "./use-get-workspace-by-id";
import { getUseListWorkspacesQueryKey } from "./use-list-wokrpsaces";

/**
 * Returns the mutation key for renaming a workspace.
 *
 * @since 0.0.1
 * @param {string} workspaceId - The ID of the workspace to rename.
 */
export const getUseRenameMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).renameWorkspace`];

/**
 * Renames a workspace.
 *
 * @param {string} workspaceId - The ID of the workspace to rename.
 * @since 0.0.1
 */
export const renameWorkspace = (workspaceId: string) => (request: RenameWorkspaceRequest) => {
  return ApiClient.renameWorkspace(workspaceId, request);
}

/**
 * Configuration for the useRenameWorkspace hook.
 *
 * @since 0.0.1
 */
export type UseRenameWorkspaceConfig = MutationConfig<ReturnType<typeof renameWorkspace>>;

/**
 * A React hook for renaming a workspace.
 *
 * @param {UseRenameWorkspaceConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useRenameWorkspace = (config: UseRenameWorkspaceConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseRenameMutationKey(workspace.id),
    mutationFn: renameWorkspace(workspace.id),

    onMutate(args) {
      log.debug(`[Workspace(${workspace.name}]): Attempting to rename workspace...`);

      config.onMutate?.(args);
    },

    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id) });
      queryClient.invalidateQueries({ queryKey: getUseListWorkspacesQueryKey() });

      log.debug(`[Workspace(${workspace.name}]): has been renamed into '${args.name}'`);

      config.onSuccess?.(result, args, ctx);
    }
  });
};
