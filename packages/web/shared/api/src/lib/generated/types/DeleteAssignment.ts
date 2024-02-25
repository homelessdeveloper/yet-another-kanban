/**
 * @description Success
*/
export type DeleteAssignment200 = any | null;

 export type DeleteAssignmentMutationResponse = any | null;

 export type DeleteAssignmentPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    assignmentId: string;
};
export type DeleteAssignmentMutation = {
    Response: DeleteAssignmentMutationResponse;
    PathParams: DeleteAssignmentPathParams;
};