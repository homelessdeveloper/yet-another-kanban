import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { UpdateAssignmentMutationRequest, UpdateAssignmentMutationResponse, UpdateAssignmentPathParams } from "../types/UpdateAssignment";
import type { UseMutationOptions } from "@tanstack/react-query";

type UpdateAssignmentClient = typeof client<UpdateAssignmentMutationResponse, never, UpdateAssignmentMutationRequest>;
type UpdateAssignment = {
    data: UpdateAssignmentMutationResponse;
    error: never;
    request: UpdateAssignmentMutationRequest;
    pathParams: UpdateAssignmentPathParams;
    queryParams: never;
    headerParams: never;
    response: UpdateAssignmentMutationResponse;
    client: {
        parameters: Partial<Parameters<UpdateAssignmentClient>[0]>;
        return: Awaited<ReturnType<UpdateAssignmentClient>>;
    };
};
/**
     * @summary Updates existing assignment
     * @link /Api/Workspaces/:workspaceId/UpdateAssignment/:assignmentId */
export function useUpdateAssignment(assignmentId: UpdateAssignmentPathParams["assignmentId"], workspaceId: UpdateAssignmentPathParams["workspaceId"], options: {
    mutation?: UseMutationOptions<UpdateAssignment["response"], UpdateAssignment["error"], UpdateAssignment["request"]>;
    client?: UpdateAssignment["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<UpdateAssignment["data"], UpdateAssignment["error"], UpdateAssignment["request"]>({
                method: "put",
                url: `/Api/Workspaces/${workspaceId}/UpdateAssignment/${assignmentId}`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}