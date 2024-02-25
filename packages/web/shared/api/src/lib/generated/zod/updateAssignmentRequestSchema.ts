import { z } from "zod";

export const updateAssignmentRequestSchema = z.object({"title": z.string().nullish(),"description": z.string().nullish()});