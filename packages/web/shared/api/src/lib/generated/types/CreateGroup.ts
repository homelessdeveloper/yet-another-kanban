import type { CreateGroupResponse } from "./CreateGroupResponse";
import type { CreateGroupRequest } from "./CreateGroupRequest";

export type CreateGroupPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
};

 /**
 * @description Success
*/
export type CreateGroup200 = CreateGroupResponse;

 export type CreateGroupMutationRequest = CreateGroupRequest;

 /**
 * @description Success
*/
export type CreateGroupMutationResponse = CreateGroupResponse;
export type CreateGroupMutation = {
    Response: CreateGroupMutationResponse;
    Request: CreateGroupMutationRequest;
    PathParams: CreateGroupPathParams;
};