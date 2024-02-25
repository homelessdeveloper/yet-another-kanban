import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { DeleteGroupMutationResponse, DeleteGroupPathParams } from "../types/DeleteGroup";
import type { UseMutationOptions } from "@tanstack/react-query";

type DeleteGroupClient = typeof client<DeleteGroupMutationResponse, never, never>;
type DeleteGroup = {
    data: DeleteGroupMutationResponse;
    error: never;
    request: never;
    pathParams: DeleteGroupPathParams;
    queryParams: never;
    headerParams: never;
    response: DeleteGroupMutationResponse;
    client: {
        parameters: Partial<Parameters<DeleteGroupClient>[0]>;
        return: Awaited<ReturnType<DeleteGroupClient>>;
    };
};
/**
     * @summary Deletes specified group
     * @link /Api/Workspaces/:workspaceId/DeleteGroup/:groupId */
export function useDeleteGroup(workspaceId: DeleteGroupPathParams["workspaceId"], groupId: DeleteGroupPathParams["groupId"], options: {
    mutation?: UseMutationOptions<DeleteGroup["response"], DeleteGroup["error"], void>;
    client?: DeleteGroup["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<DeleteGroup["data"], DeleteGroup["error"], void>({
                method: "delete",
                url: `/Api/Workspaces/${workspaceId}/DeleteGroup/${groupId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}