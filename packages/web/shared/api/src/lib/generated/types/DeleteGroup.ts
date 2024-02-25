/**
 * @description Success
*/
export type DeleteGroup200 = any | null;

 export type DeleteGroupMutationResponse = any | null;

 export type DeleteGroupPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    groupId: string;
};
export type DeleteGroupMutation = {
    Response: DeleteGroupMutationResponse;
    PathParams: DeleteGroupPathParams;
};