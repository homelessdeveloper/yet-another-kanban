import { z } from "zod";

export const createWorkspaceResponseSchema = z.object({"id": z.string().uuid()});