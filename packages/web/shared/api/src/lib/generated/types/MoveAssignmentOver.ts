/**
 * @description Success
*/
export type MoveAssignmentOver200 = any | null;

 export type MoveAssignmentOverMutationResponse = any | null;

 export type MoveAssignmentOverPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    activeAssignmentId: string;
    /**
     * @type string uuid
    */
    overAssignmentId: string;
};
export type MoveAssignmentOverMutation = {
    Response: MoveAssignmentOverMutationResponse;
    PathParams: MoveAssignmentOverPathParams;
};