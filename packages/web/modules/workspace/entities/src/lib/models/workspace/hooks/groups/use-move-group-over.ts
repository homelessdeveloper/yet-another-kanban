import z from "zod";
import { ApiClient, WorkspaceResponse } from "@yak/web/shared/api";
import log from "loglevel";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { arrayMove } from "@dnd-kit/sortable"
import { produce } from "immer";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for moving a group over another within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 *
 * @since 0.0.1
 */
export const getUseMoveGroupOverMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).moveGroupOver`]

/**
 * Moves a group over another within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the active group ID and the group to move over ID.
 * @param payload.activeGroupId - The ID of the active group to move.
 * @param payload.overGroupId - The ID of the group to move over.
 *
 * @since 0.0.1
 */
export const moveGroupOver = (workspaceId: string) => (payload: { activeGroupId: string, overGroupId: string }) => {
  return ApiClient.moveGroupOver(workspaceId, payload.activeGroupId, payload.overGroupId);
}

/**
 * Configuration for the useMoveGroupOver hook.
 *
 * @since 0.0.1
 */
export type UseMoveGroupOverMutationConfig = MutationConfig<ReturnType<typeof moveGroupOver>>;

/**
 * A React hook for moving a group over another within a workspace.
 *
 * @param {UseMoveGroupOverMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useMoveGroupOver = (config: UseMoveGroupOverMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseMoveGroupOverMutationKey(workspace.id),
    mutationFn: moveGroupOver(workspace.id),
    onMutate(args) {
      const { activeGroupId, overGroupId } = args;
      log.debug(`[Workspace(${workspace.name})]: Attempting to move [Group(${activeGroupId})] over [Group(${overGroupId})]...`)

      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      queryClient.setQueryData<WorkspaceResponse>(
        getUseGetWorkspaceByIdQueryKey(workspace.id),
        produce(response => {
          if (!response) return undefined;
          const workspace = response;

          const activeGroupIndex = workspace.groups.findIndex(g => g.id == activeGroupId);
          const overGroupIndex = workspace.groups.findIndex(g => g.id == overGroupId);

          workspace.groups = arrayMove(workspace.groups, activeGroupIndex, overGroupIndex);
        })
      )
      config?.onMutate?.(args)
    },

    onSuccess(data, args, context) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });

      log.debug(`[Workspace(${workspace.name})]: [Group(${args.activeGroupId})] has been moved over [Group(${args.overGroupId})].`)
      config.onSuccess?.(data, args, context)
    },
  })
};
