import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { DeleteGroupMutationResponse, DeleteGroupPathParams } from "../../types/DeleteGroup";

/**
     * @summary Deletes specified group
     * @link /Api/Workspaces/:workspaceId/DeleteGroup/:groupId */
export async function deleteGroup(workspaceId: DeleteGroupPathParams["workspaceId"], groupId: DeleteGroupPathParams["groupId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<DeleteGroupMutationResponse>["data"]> {
    const res = await client<DeleteGroupMutationResponse>({
        method: "delete",
        url: `/Api/Workspaces/${workspaceId}/DeleteGroup/${groupId}`,
        ...options
    });
    return res.data;
}