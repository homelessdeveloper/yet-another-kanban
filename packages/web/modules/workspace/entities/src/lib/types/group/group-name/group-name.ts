import z from "zod";

/**
 * Symbol to identify the schema type for group names.
 *
 * @since 0.0.1
 */
export const GroupNameTypeId = Symbol("GroupName");

/**
 * Type alias for the GroupName schema.
 *
 * @since 0.0.1
 */
export type GroupName = z.infer<typeof GroupName>

/**
 * Schema definition for a group name.
 *
 * @since 0.0.1
 */
export const GroupName = z
  .string()
  .min(3, { message: "Group name must be at least 3 characters long." })


