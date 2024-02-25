import log from "loglevel";
import { ApiClient } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { MODULE_NAME } from "../../../../constants";
import { getUseListWorkspacesQueryKey } from "./use-list-wokrpsaces";

/**
 * Returns the mutation key for deleting a workspace.
 *
 * @since 0.0.1
 */
export const getUseDeleteWorkspaceMutationKey = () => [`[${MODULE_NAME}]#deleteWorkspace`];

/**
 * Configuration for the useDeleteWorkspace hook.
 *
 * @since 0.0.1
 */
export type UseDeleteWorkspaceMutationConfig = MutationConfig<typeof ApiClient.deleteWorkspace>;

/**
 * A React hook for deleting a workspace mutation.
 *
 * @param config - The configuration for the mutation.
 *
 * @since 0.0.1
 */
export const useDeleteWorkspace = (config: UseDeleteWorkspaceMutationConfig = {}) => {
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseDeleteWorkspaceMutationKey(),
    mutationFn: ApiClient.deleteWorkspace,

    onMutate(workspaceId) {
      log.debug(`Attempting to delete workspace '${workspaceId}' ...`);
      config.onMutate?.(workspaceId);
    },

    onSuccess(result, workspaceId, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseListWorkspacesQueryKey() });
      log.debug(`Workspace '${workspaceId}' has been deleted.`);
      config.onSuccess?.(result, workspaceId, ctx);
    }
  });
};
