import { ApiClient } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import log from "loglevel";
import { MODULE_NAME } from "../../../../constants";
import { getUseListWorkspacesQueryKey } from "./use-list-wokrpsaces";

/**
 * Returns the mutation key for creating a workspace.
 *
 * @since 0.0.1
 */
export const getUseCreateWorkspaceMutationKey = () => [`[${MODULE_NAME}]#createWorkspace`];


/**
 * Configuration for the useCreateWorkspace hook.
 *
 * @since 0.0.1
 */
export type UseCreateWorkspaceMutationConfig = MutationConfig<typeof ApiClient.createWorkspace>;

/**
 * A React hook for creating a workspace mutation.
 *
 * @param config - The configuration for the mutation.
 *
 * @since 0.0.1
 */
export const useCreateWorkspace = (config: UseCreateWorkspaceMutationConfig = {}) => {
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseCreateWorkspaceMutationKey(),
    mutationFn: ApiClient.createWorkspace,

    onMutate(args) {
      log.debug(`Attempting to create new workspace...`);
      config.onMutate?.(args);
    },

    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseListWorkspacesQueryKey() });
      log.debug(`Workspace has been created.`);
      config.onSuccess?.(result, args, ctx);
    }
  });
};
