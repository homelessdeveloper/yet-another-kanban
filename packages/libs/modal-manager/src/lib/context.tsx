import { createContext, useContext } from "react"

/**
 * Defines the shape of the modal context.
 *
 * @since 0.0.1
 */
export type ModalContext = {

  /**
   * The ID of the modal.
   *
   * @since 0.0.1
   */
  id: string;

  /**
   * Indicates whether the modal is currently visible.
   *
   * @since 0.0.1
   */
  isVisible: boolean;

  /**
   * Function that hides modal.
   *
   * @since 0.0.1
   */
  hide: () => void;
}

/**
 * @internal
 *
 * @since 0.0.1
 */
export const modalContext = createContext<ModalContext | null>(null);

/**
 * Custom hook to access the modal context.
 * Throws an error if used outside of the "ModalManager.register" method.
 *
 * @returns  The modal context.
 * @throws {Error} If used outside of the "ModalManager.register" method.
 */
export const useModal = (): ModalContext => {
  const ctx = useContext(modalContext);
  if (!ctx) {
    throw new Error(`Could not find modal context. You should only call "useModal" hook from within "ModalManager.register" method`);
  }
  return ctx;
}
