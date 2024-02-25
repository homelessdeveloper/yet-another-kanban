import { z } from "zod";

export const deleteWorkspace204Schema = z.any();
export const deleteWorkspaceMutationResponseSchema = z.any();
export const deleteWorkspacePathParamsSchema = z.object({ "workspaceId": z.string().uuid() });