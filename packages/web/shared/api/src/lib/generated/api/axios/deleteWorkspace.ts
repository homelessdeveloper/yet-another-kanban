import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { DeleteWorkspaceMutationResponse, DeleteWorkspacePathParams } from "../../types/DeleteWorkspace";

/**
     * @description Deletes existing workspace and all groups and tasks within it.
     * @summary Deletes existing workspace
     * @link /Api/Workspaces/:workspaceId */
export async function deleteWorkspace(workspaceId: DeleteWorkspacePathParams["workspaceId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<DeleteWorkspaceMutationResponse>["data"]> {
    const res = await client<DeleteWorkspaceMutationResponse>({
        method: "delete",
        url: `/Api/Workspaces/${workspaceId}`,
        ...options
    });
    return res.data;
}