import type { WorkspaceResponse } from "./WorkspaceResponse";

export type GetWorkspaceByIdPathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
};

 /**
 * @description When workspace exists
*/
export type GetWorkspaceById200 = WorkspaceResponse;

 /**
 * @description When workspace exists
*/
export type GetWorkspaceByIdQueryResponse = WorkspaceResponse;
export type GetWorkspaceByIdQuery = {
    Response: GetWorkspaceByIdQueryResponse;
    PathParams: GetWorkspaceByIdPathParams;
};