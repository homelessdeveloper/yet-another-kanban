import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { ListWorkspacesQueryResponse } from "../../types/ListWorkspaces";

/**
     * @summary Lists all workspaces that user owns
     * @link /Api/Workspaces */
export async function listWorkspaces(options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<ListWorkspacesQueryResponse>["data"]> {
    const res = await client<ListWorkspacesQueryResponse>({
        method: "get",
        url: `/Api/Workspaces`,
        ...options
    });
    return res.data;
}