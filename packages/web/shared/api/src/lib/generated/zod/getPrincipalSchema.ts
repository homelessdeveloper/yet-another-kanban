import { z } from "zod";
import { principalResponseSchema } from "./principalResponseSchema";

export const getPrincipal200Schema = z.lazy(() => principalResponseSchema);
export const getPrincipalQueryResponseSchema = z.lazy(() => principalResponseSchema);