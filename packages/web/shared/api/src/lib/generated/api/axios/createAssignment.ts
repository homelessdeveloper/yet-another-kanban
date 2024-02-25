import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { CreateAssignmentMutationRequest, CreateAssignmentMutationResponse, CreateAssignmentPathParams } from "../../types/CreateAssignment";

/**
     * @summary Creates new assignment inside specified group.
     * @link /Api/Workspaces/:workspaceId/CreateAssignment */
export async function createAssignment(workspaceId: CreateAssignmentPathParams["workspaceId"], data?: CreateAssignmentMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<CreateAssignmentMutationResponse>["data"]> {
    const res = await client<CreateAssignmentMutationResponse, CreateAssignmentMutationRequest>({
        method: "post",
        url: `/Api/Workspaces/${workspaceId}/CreateAssignment`,
        data,
        ...options
    });
    return res.data;
}