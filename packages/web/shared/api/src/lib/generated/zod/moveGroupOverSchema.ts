import { z } from "zod";

/**
 * @description Success
 */
export const moveGroupOver200Schema = z.any();
export const moveGroupOverMutationResponseSchema = z.any();
export const moveGroupOverPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "activeGroupId": z.string().uuid(), "overGroupId": z.string().uuid() });