import { z } from "zod";

/**
 * @description Success
 */
export const deleteGroup200Schema = z.any();
export const deleteGroupMutationResponseSchema = z.any();
export const deleteGroupPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "groupId": z.string().uuid() });