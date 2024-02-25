import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { MoveAssignmentOverMutationResponse, MoveAssignmentOverPathParams } from "../../types/MoveAssignmentOver";

/**
     * @summary Changes the position of assignments within the group.
     * @link /Api/Workspaces/:workspaceId/MoveAssignmentOver/:activeAssignmentId/:overAssignmentId */
export async function moveAssignmentOver(workspaceId: MoveAssignmentOverPathParams["workspaceId"], activeAssignmentId: MoveAssignmentOverPathParams["activeAssignmentId"], overAssignmentId: MoveAssignmentOverPathParams["overAssignmentId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<MoveAssignmentOverMutationResponse>["data"]> {
    const res = await client<MoveAssignmentOverMutationResponse>({
        method: "put",
        url: `/Api/Workspaces/${workspaceId}/MoveAssignmentOver/${activeAssignmentId}/${overAssignmentId}`,
        ...options
    });
    return res.data;
}