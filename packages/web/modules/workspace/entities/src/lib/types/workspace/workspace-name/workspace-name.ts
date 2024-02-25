import z from "zod"
z.isAborted

/**
 * Symbol to identify the schema type for workspace names.
 *
 * @since 0.0.1
 */
export const WorkspaceNameTypeId = Symbol("WorkspaceName");


/**
 * Type alias for the WorkspaceName schema.
 *
 * @since 0.0.1
 */
export type WorkspaceName = z.infer<typeof WorkspaceName>

/**
 * Schema definition for a workspace name.
 *
 * @since 0.0.1
 */
export const WorkspaceName = z.string()
  .min(1, { message: "Workspace name must be at least 3 characters long" })
  .max(16, { message: "Workspace name must be 16 characters long at max" })
  .brand(WorkspaceNameTypeId);
