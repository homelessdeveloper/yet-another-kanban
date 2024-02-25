import { z } from "zod";

export const signInRequestSchema = z.object({"email": z.string().email(),"password": z.string()});