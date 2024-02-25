import type { UpdateAssignmentRequest } from "./UpdateAssignmentRequest";

/**
 * @description Success
*/
export type UpdateAssignment200 = any | null;

 export type UpdateAssignmentMutationResponse = any | null;

 export type UpdateAssignmentPathParams = {
    /**
     * @type string uuid
    */
    assignmentId: string;
    /**
     * @type string
    */
    workspaceId: string;
};

 export type UpdateAssignmentMutationRequest = UpdateAssignmentRequest;
export type UpdateAssignmentMutation = {
    Response: UpdateAssignmentMutationResponse;
    Request: UpdateAssignmentMutationRequest;
    PathParams: UpdateAssignmentPathParams;
};