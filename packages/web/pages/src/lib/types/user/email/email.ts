import z from "zod";

/**
 * Symbol representing the type ID for the Email schema.
 *
 * @since 0.0.1
 */
export const EmailTypeId = Symbol("Email");


/**
 * Represents an email address.
 *
 * @since 0.0.1
 */
export type Email = z.infer<typeof Email>
export const Email = z.string()
  .email()
  .brand(EmailTypeId);

