import { z } from "zod";

export const createAssignmentResponseSchema = z.object({"id": z.string().uuid()});