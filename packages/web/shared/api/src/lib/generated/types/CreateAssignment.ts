import type { CreateAssignmentResponse } from "./CreateAssignmentResponse";
import type { CreateAssignmentRequest } from "./CreateAssignmentRequest";

export type CreateAssignmentPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
};

 /**
 * @description Success
*/
export type CreateAssignment200 = CreateAssignmentResponse;

 export type CreateAssignmentMutationRequest = CreateAssignmentRequest;

 /**
 * @description Success
*/
export type CreateAssignmentMutationResponse = CreateAssignmentResponse;
export type CreateAssignmentMutation = {
    Response: CreateAssignmentMutationResponse;
    Request: CreateAssignmentMutationRequest;
    PathParams: CreateAssignmentPathParams;
};