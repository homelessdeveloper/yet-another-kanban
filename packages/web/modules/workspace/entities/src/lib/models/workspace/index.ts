export * from "./context";
export * from "./types";
export * from "./hooks";

import * as Context from "./context";
import * as Hooks from "./hooks";


export const WorkspaceModel = {
  ...Hooks,
  ...Context
}

