import type { WorkspaceResponse } from "./WorkspaceResponse";

/**
 * @description Returns
*/
export type ListWorkspaces200 = WorkspaceResponse[];

 /**
 * @description Returns
*/
export type ListWorkspacesQueryResponse = WorkspaceResponse[];
export type ListWorkspacesQuery = {
    Response: ListWorkspacesQueryResponse;
};