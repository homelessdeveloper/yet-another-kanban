import { z } from "zod";

export const renameGroupRequestSchema = z.object({"name": z.string()});