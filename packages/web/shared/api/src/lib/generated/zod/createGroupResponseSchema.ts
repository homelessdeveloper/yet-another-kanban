import { z } from "zod";

export const createGroupResponseSchema = z.object({"id": z.string().uuid()});