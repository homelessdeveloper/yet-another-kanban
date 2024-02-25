import type { RenameWorkspaceRequest } from "./RenameWorkspaceRequest";

/**
 * @description Nothing.
*/
export type RenameWorkspace204 = any | null;

 export type RenameWorkspaceMutationResponse = any | null;

 export type RenameWorkspacePathParams = {
    /**
     * @type string uuid
    */
    workspaceId: string;
};

 export type RenameWorkspaceMutationRequest = RenameWorkspaceRequest;
export type RenameWorkspaceMutation = {
    Response: RenameWorkspaceMutationResponse;
    Request: RenameWorkspaceMutationRequest;
    PathParams: RenameWorkspacePathParams;
};