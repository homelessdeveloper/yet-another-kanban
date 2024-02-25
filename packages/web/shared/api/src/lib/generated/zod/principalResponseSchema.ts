import { z } from "zod";

export const principalResponseSchema = z.object({"id": z.string().uuid(),"username": z.string(),"email": z.string().email()});