import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { CreateAssignmentMutationRequest, CreateAssignmentMutationResponse, CreateAssignmentPathParams } from "../types/CreateAssignment";
import type { UseMutationOptions } from "@tanstack/react-query";

type CreateAssignmentClient = typeof client<CreateAssignmentMutationResponse, never, CreateAssignmentMutationRequest>;
type CreateAssignment = {
    data: CreateAssignmentMutationResponse;
    error: never;
    request: CreateAssignmentMutationRequest;
    pathParams: CreateAssignmentPathParams;
    queryParams: never;
    headerParams: never;
    response: CreateAssignmentMutationResponse;
    client: {
        parameters: Partial<Parameters<CreateAssignmentClient>[0]>;
        return: Awaited<ReturnType<CreateAssignmentClient>>;
    };
};
/**
     * @summary Creates new assignment inside specified group.
     * @link /Api/Workspaces/:workspaceId/CreateAssignment */
export function useCreateAssignment(workspaceId: CreateAssignmentPathParams["workspaceId"], options: {
    mutation?: UseMutationOptions<CreateAssignment["response"], CreateAssignment["error"], CreateAssignment["request"]>;
    client?: CreateAssignment["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<CreateAssignment["data"], CreateAssignment["error"], CreateAssignment["request"]>({
                method: "post",
                url: `/Api/Workspaces/${workspaceId}/CreateAssignment`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}