import { z } from "zod";

export const signUpRequestSchema = z.object({"email": z.string().email(),"userName": z.string(),"password": z.string()});