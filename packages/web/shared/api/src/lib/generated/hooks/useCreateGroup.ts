import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { CreateGroupMutationRequest, CreateGroupMutationResponse, CreateGroupPathParams } from "../types/CreateGroup";
import type { UseMutationOptions } from "@tanstack/react-query";

type CreateGroupClient = typeof client<CreateGroupMutationResponse, never, CreateGroupMutationRequest>;
type CreateGroup = {
    data: CreateGroupMutationResponse;
    error: never;
    request: CreateGroupMutationRequest;
    pathParams: CreateGroupPathParams;
    queryParams: never;
    headerParams: never;
    response: CreateGroupMutationResponse;
    client: {
        parameters: Partial<Parameters<CreateGroupClient>[0]>;
        return: Awaited<ReturnType<CreateGroupClient>>;
    };
};
/**
     * @summary Creates new group inside workspace
     * @link /Api/Workspaces/:workspaceId/CreateGroup */
export function useCreateGroup(workspaceId: CreateGroupPathParams["workspaceId"], options: {
    mutation?: UseMutationOptions<CreateGroup["response"], CreateGroup["error"], CreateGroup["request"]>;
    client?: CreateGroup["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<CreateGroup["data"], CreateGroup["error"], CreateGroup["request"]>({
                method: "post",
                url: `/Api/Workspaces/${workspaceId}/CreateGroup`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}