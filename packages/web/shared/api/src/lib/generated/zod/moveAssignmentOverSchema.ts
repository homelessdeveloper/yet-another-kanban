import { z } from "zod";

/**
 * @description Success
 */
export const moveAssignmentOver200Schema = z.any();
export const moveAssignmentOverMutationResponseSchema = z.any();
export const moveAssignmentOverPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "activeAssignmentId": z.string().uuid(), "overAssignmentId": z.string().uuid() });