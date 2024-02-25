/**
 * @description Success
*/
export type MoveGroupOver200 = any | null;

 export type MoveGroupOverMutationResponse = any | null;

 export type MoveGroupOverPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    activeGroupId: string;
    /**
     * @type string uuid
    */
    overGroupId: string;
};
export type MoveGroupOverMutation = {
    Response: MoveGroupOverMutationResponse;
    PathParams: MoveGroupOverPathParams;
};