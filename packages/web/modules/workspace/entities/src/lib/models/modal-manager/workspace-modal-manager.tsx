import { ModalManager } from "@yak/libs/modal-manager"

/**
 * A modal manager specifically made to work within single workspace context.
 * This means that modal windows will be able to access workspace context
 *
 * @example
 *
 * ```tsx
 * const ModalWindow = WorkspaceModalManager.register("modal-window", () => {
 *  const {workspace} = useWorkspace(); // here we are accessing the workspace context.
 * });
 *
 * return (
 *  <div> {workspace.id} </div>
 * )
 * ```
 *
 * @since 0.0.1
 */
export const WorkspaceModalManager = new ModalManager("@yak/workspace-modal-manager");
