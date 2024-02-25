import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { UpdateAssignmentMutationRequest, UpdateAssignmentMutationResponse, UpdateAssignmentPathParams } from "../../types/UpdateAssignment";

/**
     * @summary Updates existing assignment
     * @link /Api/Workspaces/:workspaceId/UpdateAssignment/:assignmentId */
export async function updateAssignment(assignmentId: UpdateAssignmentPathParams["assignmentId"], workspaceId: UpdateAssignmentPathParams["workspaceId"], data?: UpdateAssignmentMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<UpdateAssignmentMutationResponse>["data"]> {
    const res = await client<UpdateAssignmentMutationResponse, UpdateAssignmentMutationRequest>({
        method: "put",
        url: `/Api/Workspaces/${workspaceId}/UpdateAssignment/${assignmentId}`,
        data,
        ...options
    });
    return res.data;
}