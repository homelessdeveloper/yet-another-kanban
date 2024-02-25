import { z } from "zod";

export const createGroupRequestSchema = z.object({"name": z.string()});