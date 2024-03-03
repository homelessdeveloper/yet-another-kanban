import z, { string } from "zod";

/**
 * Symbol representing the type ID for the Password schema.
 *
 * @since 0.0.1
 */
export const PasswordTypeId = Symbol("Password");


/**
 * Represents a password with specific constraints.
 *
 * @since 0.0.1
 */

export type Password = z.infer<typeof Password>;
export const Password = z.string()
  .min(8, { message: "Password must be at least 8 characters long." })
  .max(16, { message: "Password must not be longer than 16 characters."})
  .regex(/[^a-zA-Z0-9]+/g, {message:"Password must include at least 1 non-alphanumerical character."})
  .refine( 
    (val) => val.split('').some(ch => ch === ch.toLocaleUpperCase()),
    {message: "Password must include at least 1 uppercase character."}
    )
  .refine(
    (val) => val.split('').some(ch => ch === ch.toLocaleLowerCase()),
    {message: "Password must include at least 1 lowercase character."}
  ) 
  .brand(PasswordTypeId);

