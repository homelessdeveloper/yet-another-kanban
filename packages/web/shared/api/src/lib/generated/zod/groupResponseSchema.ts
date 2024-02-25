import { assignmentResponseSchema } from "./assignmentResponseSchema";
import { z } from "zod";

export const groupResponseSchema = z.object({"id": z.string().uuid(),"name": z.string(),"workspaceId": z.string().uuid(),"position": z.number(),"assignments": z.array(z.lazy(() => assignmentResponseSchema))});