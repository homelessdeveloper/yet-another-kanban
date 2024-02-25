import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { RenameGroupMutationRequest, RenameGroupMutationResponse, RenameGroupPathParams } from "../../types/RenameGroup";

/**
     * @summary Rename specified group
     * @link /Api/Workspaces/:workspaceId/RenameGroup/:groupId */
export async function renameGroup(workspaceId: RenameGroupPathParams["workspaceId"], groupId: RenameGroupPathParams["groupId"], data?: RenameGroupMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<RenameGroupMutationResponse>["data"]> {
    const res = await client<RenameGroupMutationResponse, RenameGroupMutationRequest>({
        method: "put",
        url: `/Api/Workspaces/${workspaceId}/RenameGroup/${groupId}`,
        data,
        ...options
    });
    return res.data;
}