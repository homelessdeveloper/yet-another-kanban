import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { CreateWorkspaceMutationRequest, CreateWorkspaceMutationResponse } from "../../types/CreateWorkspace";

/**
     * @summary Creates new workspace
     * @link /Api/Workspaces */
export async function createWorkspace(data?: CreateWorkspaceMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<CreateWorkspaceMutationResponse>["data"]> {
    const res = await client<CreateWorkspaceMutationResponse, CreateWorkspaceMutationRequest>({
        method: "post",
        url: `/Api/Workspaces`,
        data,
        ...options
    });
    return res.data;
}