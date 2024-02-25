import { z } from "zod";
import { updateAssignmentRequestSchema } from "./updateAssignmentRequestSchema";

/**
 * @description Success
 */
export const updateAssignment200Schema = z.any();
export const updateAssignmentMutationResponseSchema = z.any();
export const updateAssignmentPathParamsSchema = z.object({ "assignmentId": z.string().uuid(), "workspaceId": z.string() });
export const updateAssignmentMutationRequestSchema = z.lazy(() => updateAssignmentRequestSchema);