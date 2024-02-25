import { z } from "zod";
import { workspaceResponseSchema } from "./workspaceResponseSchema";

/**
 * @description Returns
 */
export const listWorkspaces200Schema = z.array(z.lazy(() => workspaceResponseSchema));

 /**
       * @description Returns
       */
export const listWorkspacesQueryResponseSchema = z.array(z.lazy(() => workspaceResponseSchema));