import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { MoveGroupOverMutationResponse, MoveGroupOverPathParams } from "../../types/MoveGroupOver";

/**
     * @summary Used to swap the position of two groups within a workspace.
     * @link /Api/Workspaces/:workspaceId/MoveGroupOver/:activeGroupId/:overGroupId */
export async function moveGroupOver(workspaceId: MoveGroupOverPathParams["workspaceId"], activeGroupId: MoveGroupOverPathParams["activeGroupId"], overGroupId: MoveGroupOverPathParams["overGroupId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<MoveGroupOverMutationResponse>["data"]> {
    const res = await client<MoveGroupOverMutationResponse>({
        method: "patch",
        url: `/Api/Workspaces/${workspaceId}/MoveGroupOver/${activeGroupId}/${overGroupId}`,
        ...options
    });
    return res.data;
}