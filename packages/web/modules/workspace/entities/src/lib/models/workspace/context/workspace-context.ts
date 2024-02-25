import { createContext, useContext } from "react";
import { Draft } from "immer";
import * as API from "@yak/web/shared/api";
import { DraggedItem } from "../types";

/**
 * The state of the workspace
 *
 * @since 0.0.1
 */
export type WorkspaceState = {
  /**
   * The item being dragged. Can be:
   * - None: Nothing is being dragged;
   * - Group: Means that group is being dragged;
   * - Assignment: Means that assignment is being dragged.
   */
  draggedItem: DraggedItem;
};

/**
 * The context object providing access to the workspace data and state.
 *
 * @since 0.0.1
 */
export type WorkspaceContext = {
  workspace: API.WorkspaceResponse;
  state: WorkspaceState;
  /**
   * Set the new state of the workspace using Immer's `produce` function.
   *
   * @param cb -  A function that produces a new state based on the draft.
   * @since 0.0.1
   */
  setState: (cb: (workspace: Draft<WorkspaceState>) => void) => void;
};

/**
 * The context object for the workspace, initially set to `null`.
 *
 * @since 0.0.1
 */
export const workspaceContext = createContext<WorkspaceContext | null>(null);

/**
 * A hook to access the workspace context.
 * Throws an error if the context is null, indicating improper context setup.
 *
 * @since 0.0.1
 */
export const useWorkspace = (): WorkspaceContext => {
  const ctx = useContext(workspaceContext);
  if (ctx === null) {
    throw new Error("Workspace context is not found. Make sure you have a WorkspaceContextProvider in your component tree.");
  }
  return ctx;
};
