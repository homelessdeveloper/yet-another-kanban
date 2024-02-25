import type { RenameGroupRequest } from "./RenameGroupRequest";

/**
 * @description Success
*/
export type RenameGroup200 = any | null;

 export type RenameGroupMutationResponse = any | null;

 export type RenameGroupPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type string uuid
    */
    groupId: string;
};

 export type RenameGroupMutationRequest = RenameGroupRequest;
export type RenameGroupMutation = {
    Response: RenameGroupMutationResponse;
    Request: RenameGroupMutationRequest;
    PathParams: RenameGroupPathParams;
};