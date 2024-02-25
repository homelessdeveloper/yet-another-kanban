import z from "zod";

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
  .min(8, { message: "Password must be at least 8 characters long" })
  .brand(PasswordTypeId);

