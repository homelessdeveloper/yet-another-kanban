import z from "zod";

/**
 * Symbol to identify the schema type for assignment titles.
 *
 * @since 0.0.1
 */
export const AssignmentTitleTypeId = Symbol("AssignmentTitle");

/**
 * Type alias for the AssignmentTitle schema.
 *
 * @since 0.0.1
 */
export type AssignmentTitle = z.infer<typeof AssignmentTitle>

/**
 * Schema definition for an assignment title.
 *
 * @since 0.0.1
 */
export const AssignmentTitle = z.string()
  .min(1, { message: "Assignment title cannot be empty." })
  .brand(AssignmentTitleTypeId)


