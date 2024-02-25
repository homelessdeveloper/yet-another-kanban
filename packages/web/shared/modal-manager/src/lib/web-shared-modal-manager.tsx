import { ModalManager } from "@yak/libs/modal-manager";

/**
 * Manages global modal instances in the application.
 * Allows registration of modal instances that can be opened and closed.
 *
 * @since 0.0.1
 */
export const GlobalModalManager = new ModalManager("@yak/global-modal-manager");
