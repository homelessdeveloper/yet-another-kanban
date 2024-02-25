import { z } from "zod";
import { createGroupResponseSchema } from "./createGroupResponseSchema";
import { createGroupRequestSchema } from "./createGroupRequestSchema";

export const createGroupPathParamsSchema = z.object({ "workspaceId": z.string().uuid() });

 /**
       * @description Success
       */
export const createGroup200Schema = z.lazy(() => createGroupResponseSchema);
export const createGroupMutationRequestSchema = z.lazy(() => createGroupRequestSchema);

 /**
       * @description Success
       */
export const createGroupMutationResponseSchema = z.lazy(() => createGroupResponseSchema);