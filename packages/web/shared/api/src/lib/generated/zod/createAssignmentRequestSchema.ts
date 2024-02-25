import { z } from "zod";

export const createAssignmentRequestSchema = z.object({"title": z.string(),"groupId": z.string().uuid(),"description": z.string().nullish()});