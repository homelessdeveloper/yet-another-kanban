import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { MoveAssignmentToMutationResponse, MoveAssignmentToPathParams } from "../../types/MoveAssignmentTo";

/**
     * @summary Used to change the position of assignments within the group.
     * @link /Api/Workspaces/:workspaceId/MoveAssignmentTo/:assignmentId/:groupId */
export async function moveAssignmentTo(workspaceId: MoveAssignmentToPathParams["workspaceId"], assignmentId: MoveAssignmentToPathParams["assignmentId"], groupId: MoveAssignmentToPathParams["groupId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<MoveAssignmentToMutationResponse>["data"]> {
    const res = await client<MoveAssignmentToMutationResponse>({
        method: "put",
        url: `/Api/Workspaces/${workspaceId}/MoveAssignmentTo/${assignmentId}/${groupId}`,
        ...options
    });
    return res.data;
}