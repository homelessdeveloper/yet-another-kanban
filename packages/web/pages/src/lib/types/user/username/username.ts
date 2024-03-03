import z from "zod";

/**
 * Symbol representing the type ID for the UserName schema.
 *
 * @since 0.0.1
 */
export const UserNameTypeId = Symbol("UserName");


/**
 * Represents a user name with specific constraints.
 *
 * @since 0.0.1
 */
export type UserName = z.infer<typeof UserName>;
export const UserName = z.string()
  .min(5, { message: "Username must be at least 5 characters long" })
  .regex(/^[a-zA-Z\s]+$/, { message:"All username characters must be alphabetical"})
  .max(16, { message: "Username must be 16 characters long at max" })
  .brand(UserNameTypeId);

