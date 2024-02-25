import { z } from "zod";

export const assignmentResponseSchema = z.object({"id": z.string().uuid(),"title": z.string(),"description": z.string().nullish(),"position": z.number()});