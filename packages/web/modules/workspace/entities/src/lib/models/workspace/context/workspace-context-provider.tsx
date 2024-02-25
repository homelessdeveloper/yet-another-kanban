import { FC, PropsWithChildren, useState } from "react";
import { WorkspaceContext, WorkspaceState, workspaceContext } from "./workspace-context";
import { DraggedItem } from "../types";
import { WorkspaceModalManager } from "../../modal-manager";
import { produce } from "immer";
import { WorkspaceModel } from "..";

/**
 * Props for the WorkspaceContextProvider component.
 *
 * @param props - The component props.
 * @since 0.0.1
 */
export type WorkspaceContextProviderProps = PropsWithChildren<{ workspaceId: string }>;

/**
 * A component that provides the workspace context to its descendants.
 *
 * @param  props - The component props.
 * @since 0.0.1
 */
export const WorkspaceContextProvider: FC<WorkspaceContextProviderProps> = (props) => {
  const { workspaceId, children } = props;

  const getWorkspaceQuery = WorkspaceModel.useGetWorkspaceById(workspaceId);
  const workspace = getWorkspaceQuery.data;
  const [state, setWorkspaceState] = useState<WorkspaceState>({
    draggedItem: DraggedItem.None()
  });

  /**
   * Sets the state of the workspace context using Immer's produce function.
   *
   * @since 0.0.1
   */
  const setState: WorkspaceContext["setState"] = cb => setWorkspaceState(produce(cb));

  return (
    <>
      {workspace && (
        <workspaceContext.Provider
          value={{
            workspace,
            state,
            setState
          }}
        >
          <WorkspaceModalManager.Provider />
          {children}
        </workspaceContext.Provider>
      )}
    </>
  );
};
