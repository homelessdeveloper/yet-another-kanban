import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { DeleteWorkspaceMutationResponse, DeleteWorkspacePathParams } from "../types/DeleteWorkspace";
import type { UseMutationOptions } from "@tanstack/react-query";

type DeleteWorkspaceClient = typeof client<DeleteWorkspaceMutationResponse, never, never>;
type DeleteWorkspace = {
    data: DeleteWorkspaceMutationResponse;
    error: never;
    request: never;
    pathParams: DeleteWorkspacePathParams;
    queryParams: never;
    headerParams: never;
    response: DeleteWorkspaceMutationResponse;
    client: {
        parameters: Partial<Parameters<DeleteWorkspaceClient>[0]>;
        return: Awaited<ReturnType<DeleteWorkspaceClient>>;
    };
};
/**
     * @description Deletes existing workspace and all groups and tasks within it.
     * @summary Deletes existing workspace
     * @link /Api/Workspaces/:workspaceId */
export function useDeleteWorkspace(workspaceId: DeleteWorkspacePathParams["workspaceId"], options: {
    mutation?: UseMutationOptions<DeleteWorkspace["response"], DeleteWorkspace["error"], void>;
    client?: DeleteWorkspace["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<DeleteWorkspace["data"], DeleteWorkspace["error"], void>({
                method: "delete",
                url: `/Api/Workspaces/${workspaceId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}