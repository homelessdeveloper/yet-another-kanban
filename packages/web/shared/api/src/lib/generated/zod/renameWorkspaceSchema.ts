import { z } from "zod";
import { renameWorkspaceRequestSchema } from "./renameWorkspaceRequestSchema";

/**
 * @description Nothing.
 */
export const renameWorkspace204Schema = z.any();
export const renameWorkspaceMutationResponseSchema = z.any();
export const renameWorkspacePathParamsSchema = z.object({ "workspaceId": z.string().uuid() });
export const renameWorkspaceMutationRequestSchema = z.lazy(() => renameWorkspaceRequestSchema);