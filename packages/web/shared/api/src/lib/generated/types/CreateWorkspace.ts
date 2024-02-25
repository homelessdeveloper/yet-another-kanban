import type { CreateWorkspaceResponse } from "./CreateWorkspaceResponse";
import type { CreateWorkspaceRequest } from "./CreateWorkspaceRequest";

export type CreateWorkspace200 = CreateWorkspaceResponse;

 export type CreateWorkspaceMutationRequest = CreateWorkspaceRequest;

 export type CreateWorkspaceMutationResponse = CreateWorkspaceResponse;
export type CreateWorkspaceMutation = {
    Response: CreateWorkspaceMutationResponse;
    Request: CreateWorkspaceMutationRequest;
};