import { z } from "zod";
import { authResponseSchema } from "./authResponseSchema";
import { signUpRequestSchema } from "./signUpRequestSchema";

export const signUp200Schema = z.lazy(() => authResponseSchema);
export const signUpMutationRequestSchema = z.lazy(() => signUpRequestSchema);
export const signUpMutationResponseSchema = z.lazy(() => authResponseSchema);