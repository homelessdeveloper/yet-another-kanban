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
  .min(1, { message: "Username must be at least 3 characters long" })
  .max(255, { message: "Username must be 255 characters long at max" })
  .brand(UserNameTypeId);

