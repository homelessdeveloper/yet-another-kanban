import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { RenameGroupMutationRequest, RenameGroupMutationResponse, RenameGroupPathParams } from "../types/RenameGroup";
import type { UseMutationOptions } from "@tanstack/react-query";

type RenameGroupClient = typeof client<RenameGroupMutationResponse, never, RenameGroupMutationRequest>;
type RenameGroup = {
    data: RenameGroupMutationResponse;
    error: never;
    request: RenameGroupMutationRequest;
    pathParams: RenameGroupPathParams;
    queryParams: never;
    headerParams: never;
    response: RenameGroupMutationResponse;
    client: {
        parameters: Partial<Parameters<RenameGroupClient>[0]>;
        return: Awaited<ReturnType<RenameGroupClient>>;
    };
};
/**
     * @summary Rename specified group
     * @link /Api/Workspaces/:workspaceId/RenameGroup/:groupId */
export function useRenameGroup(workspaceId: RenameGroupPathParams["workspaceId"], groupId: RenameGroupPathParams["groupId"], options: {
    mutation?: UseMutationOptions<RenameGroup["response"], RenameGroup["error"], RenameGroup["request"]>;
    client?: RenameGroup["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<RenameGroup["data"], RenameGroup["error"], RenameGroup["request"]>({
                method: "put",
                url: `/Api/Workspaces/${workspaceId}/RenameGroup/${groupId}`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}