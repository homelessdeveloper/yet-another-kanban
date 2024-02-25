import { Subject } from "rxjs";
import { Data, Match } from "effect";

/**
 * Represents different types of events that can occur in the API client.
 *
 * @since 0.0.1
 */
export type ApiClientEvent = Data.TaggedEnum<{
  /**
   * Event indicating that the JWT token has expired.
   *
   * @since 0.0.1
   */
  TokenExpired: {} //
}>;

/**
 * Factory for creating instances of ApiClientEvent.
 *
 * @since 0.0.1
 */
export const ApiClientEvent = Data.taggedEnum<ApiClientEvent>();

/**
 * Operations related to handling events within the API client.
 *
 * @since 0.0.1
 */
export module EventsOps {

  /**
   * Subject for publishing and subscribing to API client events.
   *
   * @since 0.0.1
   */
  export const events$ = new Subject<ApiClientEvent>();

  /**
   * Subscribe to the 'TokenExpired' event and execute a callback function when it occurs.
   * @param cb The callback function to execute when the 'TokenExpired' event occurs.
   * @returns A subscription object that can be used to unsubscribe from the event.
   *
   * @since 0.0.1
   */
  export const onTokenExpired = (cb: () => void) => {
    return events$.subscribe(
      // Match the event type and execute the corresponding callback.
      Match.valueTags({
        TokenExpired: cb
      })
    );
  }

}
