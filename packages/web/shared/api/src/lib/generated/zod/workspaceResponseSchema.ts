import { groupResponseSchema } from "./groupResponseSchema";
import { z } from "zod";

export const workspaceResponseSchema = z.object({"id": z.string().uuid(),"name": z.string(),"groups": z.array(z.lazy(() => groupResponseSchema))});