import { z } from "zod";
import { renameGroupRequestSchema } from "./renameGroupRequestSchema";

/**
 * @description Success
 */
export const renameGroup200Schema = z.any();
export const renameGroupMutationResponseSchema = z.any();
export const renameGroupPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "groupId": z.string().uuid() });
export const renameGroupMutationRequestSchema = z.lazy(() => renameGroupRequestSchema);