import { z } from "zod";
import { workspaceResponseSchema } from "./workspaceResponseSchema";

export const getWorkspaceByIdPathParamsSchema = z.object({ "workspaceId": z.string().uuid() });

 /**
       * @description When workspace exists
       */
export const getWorkspaceById200Schema = z.lazy(() => workspaceResponseSchema);

 /**
       * @description When workspace exists
       */
export const getWorkspaceByIdQueryResponseSchema = z.lazy(() => workspaceResponseSchema);