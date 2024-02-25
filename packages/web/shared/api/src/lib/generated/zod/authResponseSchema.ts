import { z } from "zod";

export const authResponseSchema = z.object({"username": z.string(),"email": z.string().email(),"token": z.string()});