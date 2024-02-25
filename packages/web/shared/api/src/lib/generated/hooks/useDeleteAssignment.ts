import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { DeleteAssignmentMutationResponse, DeleteAssignmentPathParams } from "../types/DeleteAssignment";
import type { UseMutationOptions } from "@tanstack/react-query";

type DeleteAssignmentClient = typeof client<DeleteAssignmentMutationResponse, never, never>;
type DeleteAssignment = {
    data: DeleteAssignmentMutationResponse;
    error: never;
    request: never;
    pathParams: DeleteAssignmentPathParams;
    queryParams: never;
    headerParams: never;
    response: DeleteAssignmentMutationResponse;
    client: {
        parameters: Partial<Parameters<DeleteAssignmentClient>[0]>;
        return: Awaited<ReturnType<DeleteAssignmentClient>>;
    };
};
/**
     * @summary Deletes specified assignment
     * @link /Api/Workspaces/:workspaceId/DeleteAssignment/:assignmentId */
export function useDeleteAssignment(workspaceId: DeleteAssignmentPathParams["workspaceId"], assignmentId: DeleteAssignmentPathParams["assignmentId"], options: {
    mutation?: UseMutationOptions<DeleteAssignment["response"], DeleteAssignment["error"], void>;
    client?: DeleteAssignment["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<DeleteAssignment["data"], DeleteAssignment["error"], void>({
                method: "delete",
                url: `/Api/Workspaces/${workspaceId}/DeleteAssignment/${assignmentId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}