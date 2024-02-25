import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { CreateGroupMutationRequest, CreateGroupMutationResponse, CreateGroupPathParams } from "../../types/CreateGroup";

/**
     * @summary Creates new group inside workspace
     * @link /Api/Workspaces/:workspaceId/CreateGroup */
export async function createGroup(workspaceId: CreateGroupPathParams["workspaceId"], data?: CreateGroupMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<CreateGroupMutationResponse>["data"]> {
    const res = await client<CreateGroupMutationResponse, CreateGroupMutationRequest>({
        method: "post",
        url: `/Api/Workspaces/${workspaceId}/CreateGroup`,
        data,
        ...options
    });
    return res.data;
}