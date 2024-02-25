import { z } from "zod";

export const renameWorkspaceRequestSchema = z.object({"name": z.string()});