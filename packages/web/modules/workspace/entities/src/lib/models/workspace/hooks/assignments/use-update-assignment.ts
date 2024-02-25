import log from "loglevel";
import z from "zod";
import { ApiClient, UpdateAssignmentRequest } from "@yak/web/shared/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { MutationConfig } from "@yak/libs/react-query";
import { useWorkspace } from "../../context";
import { getUseGetWorkspaceByIdQueryKey } from "../workspace";

/**
 * Returns the mutation key for updating an assignment within a workspace.
 *
 * @param  workspaceId - The ID of the workspace.
 * @since 0.0.1
 */
export const getUseUpdateAssignmentMutationKey = (workspaceId: string) => [`workspaces(${workspaceId}).updateAssignment`] as const;


/**
 * Represents payload data that is necessary to update assignment.
 *
 * @since 0.0.1
 */
export type UpdateAssignmentPayload = UpdateAssignmentRequest & {
  /*
   * The ID of the assignment that we want to update
   */
  assignmentId: string,
}


/**
 * Updates an assignment within a workspace.
 *
 * @since 0.0.1
 * @param workspaceId - The ID of the workspace.
 * @param request - The request body containing the updated assignment details.
 * @param request.assignmentId - The ID of the assignment to update.
 * @param request.name - The updated name of the assignment.
 * @param request.description - The updated description of the assignment.
 */
export const updateAssignment = (workspaceId: string) => (payload: UpdateAssignmentPayload) => {
  return ApiClient.updateAssignment(payload.assignmentId, workspaceId, payload);
};

/**
 * Configuration for the useUpdateAssignment mutation.
 *
 * @since 0.0.1
 */
export type UseUpdateAssignmentMutationConfig = MutationConfig<ReturnType<typeof updateAssignment>>;

/**
 * A React hook for updating an assignment within a workspace.
 *
 * @param {UseUpdateAssignmentMutationConfig} config - The configuration for the mutation.
 * @since 0.0.1
 */
export const useUpdateAssignment = (config: UseUpdateAssignmentMutationConfig = {}) => {
  const { workspace } = useWorkspace();
  const queryClient = useQueryClient();

  return useMutation({
    ...config,
    mutationKey: getUseUpdateAssignmentMutationKey(workspace.id),
    mutationFn: updateAssignment(workspace.id),
    onMutate(args) {
      log.debug(`[Workspace(${workspace.name})]: Attempting to update Assignment(${args.assignmentId})`, args);

      config.onMutate?.(args);
    },
    onSuccess(result, args, ctx) {
      queryClient.invalidateQueries({ queryKey: getUseGetWorkspaceByIdQueryKey(workspace.id), });
      log.debug(`[Workspace(${workspace.name})]: Assignment(${args.assignmentId}) has been successfully updated.`, args);

      config.onSuccess?.(result, args, ctx);
    }
  });
};
