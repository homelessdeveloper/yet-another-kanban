import { z } from "zod";

export const createWorkspaceRequestSchema = z.object({"name": z.string()});