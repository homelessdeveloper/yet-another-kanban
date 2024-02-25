import log from "loglevel";
import { ApiClient, CreateGroupRequest } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for creating a group within a workspace.
 *
 * @param  workspaceId - The ID of the workspace.
 * @since 0.0.1
 */
export const getUseCreateGroupMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).createGroup`];

/**
 * Creates a new group within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param request - The request to create the group.
 * @since 0.0.1
 */
export const createGroup = (workspaceId: string) => (request: CreateGroupRequest) => {
  return ApiClient.createGroup(workspaceId, request);
}

/**
 * Configuration for the useCreateGroup hook.
 *
 * @since 0.0.1
 */
export type UseCreateGroupMutationConfig = MutationConfig<ReturnType<typeof createGroup>>;

/**
 * A React hook for creating a group within a workspace.
 *
 * @param config - The configuration for the mutation.
 *
 * @since 0.0.1
 */
export const useCreateGroup = (config: UseCreateGroupMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseCreateGroupMutationKey(workspace.id),
    mutationFn: createGroup(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to create group with name: '${args.name}'`)

      config.onMutate?.(args);
    },

    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id) });
      log.debug(`[Workspace(${workspace.name})]: Group ${args.name} has been successfully created.`)
      config.onSuccess?.(result, args, ctx);
    }
  });
};
