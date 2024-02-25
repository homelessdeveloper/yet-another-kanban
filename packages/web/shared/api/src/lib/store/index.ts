import { create } from "zustand"
import { devtools, persist } from 'zustand/middleware'
import { MODULE_NAME } from "../constants";

/**
 * Represents the state managed by the store in the API client.
 *
 * @since 0.0.1
 */
export type ApiClientState = {
  token: string | null, // The JWT token used for authentication, or null if not authenticated.
  setToken: (token: string | null) => void // A function to update the token in the store.
}

/**
 * Operations related to managing the store in the API client.
 *
 * @since 0.0.1
 */
export module StoreOps {

  /**
   * The Zustand store for managing the API client state.
   *
   * @since 0.0.1
   */
  export const store = create<ApiClientState>()(
    devtools(
      persist(
        (set) => ({
          token: null,
          setToken: token => set(state => ({ ...state, token })),
        }),
        { name: MODULE_NAME }
      ),
      { name: MODULE_NAME, }
    )
  );

  /**
   * Get the current JWT token from the store.
   * @returns The current JWT token, or null if not authenticated.
   *
   * @since 0.0.1
   */
  export const getToken = () => store.getState().token;

  /**
   * Update the JWT token in the store.
   * @param token The new JWT token to set, or null to clear the token.
   *
   * @since 0.0.1
   */
  export const setToken = (token: string | null) => store.getState().setToken(token);
}
