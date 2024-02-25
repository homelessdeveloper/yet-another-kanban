import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { CreateWorkspaceMutationRequest, CreateWorkspaceMutationResponse } from "../types/CreateWorkspace";
import type { UseMutationOptions } from "@tanstack/react-query";

type CreateWorkspaceClient = typeof client<CreateWorkspaceMutationResponse, never, CreateWorkspaceMutationRequest>;
type CreateWorkspace = {
    data: CreateWorkspaceMutationResponse;
    error: never;
    request: CreateWorkspaceMutationRequest;
    pathParams: never;
    queryParams: never;
    headerParams: never;
    response: CreateWorkspaceMutationResponse;
    client: {
        parameters: Partial<Parameters<CreateWorkspaceClient>[0]>;
        return: Awaited<ReturnType<CreateWorkspaceClient>>;
    };
};
/**
     * @summary Creates new workspace
     * @link /Api/Workspaces */
export function useCreateWorkspace(options: {
    mutation?: UseMutationOptions<CreateWorkspace["response"], CreateWorkspace["error"], CreateWorkspace["request"]>;
    client?: CreateWorkspace["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<CreateWorkspace["data"], CreateWorkspace["error"], CreateWorkspace["request"]>({
                method: "post",
                url: `/Api/Workspaces`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}