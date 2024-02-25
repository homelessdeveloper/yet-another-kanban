import log from "loglevel";
import { ApiClient, CreateAssignmentRequest } from "@yak/web/shared/api"
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for creating an assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 *
 * @since 0.0.1
 */
export const getUseCreateAssignmentMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).createAssignment`];

/**
 * The payload needed to create new assignment
 *
 * @since 0.0.1
 */
export type CreateAssignmentPayload = {

  /**
   * The id of the group in which assignment will be made.
   */
  groupId: string,

  /**
   * The data necessary to create new assignment.
   */
  request: CreateAssignmentRequest
};

/**
 * Creates a new assignment within a workspace.
 *
 * @param workspaceId - The ID of the workspace.
 * @param payload - The payload containing the group ID and request data.
 * @param payload.groupId - The ID of the group to which the assignment belongs.
 * @param payload.request - The request data for creating the assignment.
 *
 * @since 0.0.1
 */
export const createAssignment = (workspaceId: string) => ({ request }: CreateAssignmentPayload) => {
  return ApiClient.createAssignment(workspaceId, request);
}

/**
 * Configuration for the useCreateAssignment hook.
 *
 * @since 0.0.1
 */
export type UseCreateAssignmentMutationConfig = MutationConfig<ReturnType<typeof createAssignment>>;

/**
 * A React hook for creating an assignment within a workspace.
 *
 * @param config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useCreateAssignment = (config: UseCreateAssignmentMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseCreateAssignmentMutationKey(workspace.id),
    mutationFn: createAssignment(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to create assignment...`, args);
      config.onMutate?.(args);
    },
    onSuccess(response, payload, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`[Workspace(${workspace.name})]: Assignment with id '${response.id}' has been successfully created.`)
      config.onSuccess?.(response, payload, ctx);
    }
  });
};
