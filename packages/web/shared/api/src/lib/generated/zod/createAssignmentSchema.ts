import { z } from "zod";
import { createAssignmentResponseSchema } from "./createAssignmentResponseSchema";
import { createAssignmentRequestSchema } from "./createAssignmentRequestSchema";

export const createAssignmentPathParamsSchema = z.object({ "workspaceId": z.string().uuid() });

 /**
       * @description Success
       */
export const createAssignment200Schema = z.lazy(() => createAssignmentResponseSchema);
export const createAssignmentMutationRequestSchema = z.lazy(() => createAssignmentRequestSchema);

 /**
       * @description Success
       */
export const createAssignmentMutationResponseSchema = z.lazy(() => createAssignmentResponseSchema);