import { z } from "zod";
import { authResponseSchema } from "./authResponseSchema";
import { signInRequestSchema } from "./signInRequestSchema";

export const signIn200Schema = z.lazy(() => authResponseSchema);
export const signInMutationRequestSchema = z.lazy(() => signInRequestSchema);
export const signInMutationResponseSchema = z.lazy(() => authResponseSchema);