import { z } from "zod";

/**
 * @description Success
 */
export const deleteAssignment200Schema = z.any();
export const deleteAssignmentMutationResponseSchema = z.any();
export const deleteAssignmentPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "assignmentId": z.string().uuid() });