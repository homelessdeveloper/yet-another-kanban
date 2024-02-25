import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { DeleteAssignmentMutationResponse, DeleteAssignmentPathParams } from "../../types/DeleteAssignment";

/**
     * @summary Deletes specified assignment
     * @link /Api/Workspaces/:workspaceId/DeleteAssignment/:assignmentId */
export async function deleteAssignment(workspaceId: DeleteAssignmentPathParams["workspaceId"], assignmentId: DeleteAssignmentPathParams["assignmentId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<DeleteAssignmentMutationResponse>["data"]> {
    const res = await client<DeleteAssignmentMutationResponse>({
        method: "delete",
        url: `/Api/Workspaces/${workspaceId}/DeleteAssignment/${assignmentId}`,
        ...options
    });
    return res.data;
}