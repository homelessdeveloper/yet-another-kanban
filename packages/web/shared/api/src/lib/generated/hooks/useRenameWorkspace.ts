import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { RenameWorkspaceMutationRequest, RenameWorkspaceMutationResponse, RenameWorkspacePathParams } from "../types/RenameWorkspace";
import type { UseMutationOptions } from "@tanstack/react-query";

type RenameWorkspaceClient = typeof client<RenameWorkspaceMutationResponse, never, RenameWorkspaceMutationRequest>;
type RenameWorkspace = {
    data: RenameWorkspaceMutationResponse;
    error: never;
    request: RenameWorkspaceMutationRequest;
    pathParams: RenameWorkspacePathParams;
    queryParams: never;
    headerParams: never;
    response: RenameWorkspaceMutationResponse;
    client: {
        parameters: Partial<Parameters<RenameWorkspaceClient>[0]>;
        return: Awaited<ReturnType<RenameWorkspaceClient>>;
    };
};
/**
     * @summary Rename workspace by id
     * @link /Api/Workspaces/:workspaceId */
export function useRenameWorkspace(workspaceId: RenameWorkspacePathParams["workspaceId"], options: {
    mutation?: UseMutationOptions<RenameWorkspace["response"], RenameWorkspace["error"], RenameWorkspace["request"]>;
    client?: RenameWorkspace["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<RenameWorkspace["data"], RenameWorkspace["error"], RenameWorkspace["request"]>({
                method: "post",
                url: `/Api/Workspaces/${workspaceId}`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}