import * as Generated from "./generated";
import { EventsOps } from "./events"
import { StoreOps } from "./store"

/**
 * The main API client object combining generated API methods, event operations,
 * and store operations.
 *
 * @since 0.0.1
 */
export const ApiClient = {
  ...Generated,
  ...EventsOps,
  ...StoreOps
};


