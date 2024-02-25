/**
 * @description Success
*/
export type MoveAssignmentTo200 = any | null;

 export type MoveAssignmentToMutationResponse = any | null;

 export type MoveAssignmentToPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    assignmentId: string;
    /**
     * @type string uuid
    */
    groupId: string;
};
export type MoveAssignmentToMutation = {
    Response: MoveAssignmentToMutationResponse;
    PathParams: MoveAssignmentToPathParams;
};