import { z } from "zod";

/**
 * @description Success
 */
export const moveAssignmentTo200Schema = z.any();
export const moveAssignmentToMutationResponseSchema = z.any();
export const moveAssignmentToPathParamsSchema = z.object({ "workspaceId": z.string().uuid(), "assignmentId": z.string().uuid(), "groupId": z.string().uuid() });