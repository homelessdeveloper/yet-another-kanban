import { z } from "zod";
import { createWorkspaceResponseSchema } from "./createWorkspaceResponseSchema";
import { createWorkspaceRequestSchema } from "./createWorkspaceRequestSchema";

export const createWorkspace200Schema = z.lazy(() => createWorkspaceResponseSchema);
export const createWorkspaceMutationRequestSchema = z.lazy(() => createWorkspaceRequestSchema);
export const createWorkspaceMutationResponseSchema = z.lazy(() => createWorkspaceResponseSchema);