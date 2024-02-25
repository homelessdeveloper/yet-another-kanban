import log from "loglevel";
import { FC } from "react";
import { StoreApi, UseBoundStore, create } from "zustand";
import { ModalState } from "./modal-state";
import { devtools } from "zustand/middleware";
import { produce } from "immer";
import { modalContext, useModal } from "./context";
import { ModalModel } from "./modal-model";

/**
 * Represents the state of all modals managed by the `ModalManager`.
 *
 * @since 0.0.1
 */
export type ModalManagerState = Record<string, ModalState>;

/**
 * Manages the registration, showing, and hiding of modals within an application.
 * Each instance of `ModalManager` manages a unique set of modals identified by a `name`.
 *
 * @since 0.0.1
 */
export class ModalManager {

  /**
   * The name of the manager, which must be globally unique across the application.
   *
   * @since 0.0.1
   */
  public readonly name: string;

  /**
   * A collection of modal components registered with this manager.
   *
   * @since 0.0.1
   */
  private readonly components: Record<string, FC<unknown>> = {};

  /**
   * The state management instance for the modals managed by this manager.
   *
   * @since 0.0.1
   */
  private readonly state: UseBoundStore<StoreApi<Record<string, ModalState>>>;

  /**
   * Creates a new `ModalManager` instance with the given `name`.
   * @param name The name of the manager, which must be globally unique.
   *
   * @since 0.0.1
   */
  public constructor(name: string) {
    this.name = name;
    this.state = create<Record<string, ModalState>>()(
      devtools(() => ({}), { name })
    );

    log.debug(`ModalManager(${this.name}) has been created.`);
  }

  /**
   * Registers a modal component with the manager.
   * @param id The unique identifier for the modal.
   * @param Component The React component representing the modal.
   * @returns A `ModalModel` instance for the registered modal.
   * @throws If a modal with the same `id` already exists.
   *
   * @since 0.0.1
   */
  public readonly register = <Props extends object = {}>(id: string, Component: FC<Props>) => {
    if (this.components[id]) {
      throw new Error(`Modal with id: ${id} already exists. Please choose another id.`);
    }

    this.components[id] = Component;
    this.state.setState(produce(state => { state[id] = ModalState.Hidden({ props: {} }) }));

    log.debug(`[ModalManager(${this.name})]: modal with id '${id}' has been registered.`);
    return new ModalModel<Props>(id, this);
  }

  /**
   * Shows a modal with the specified `id` and `props`.
   *
   * @param id The unique identifier of the modal to show.
   * @param props The props to pass to the modal component.
   *
   * @internal
   *
   * @since 0.0.1
   */
  public readonly show = (id: string, props: unknown) => this.state.setState(produce(state => {
    if (!state[id]) throw new Error(`Modal with id: '${id}' does not exist`);
    state[id] = ModalState.Visible({ props });

    log.debug(`[ModalManager(${this.name})]: modal with id '${id}' has been opened.`);
  }));

  /**
   * Hides a modal with the specified `id`.
   * @param id The unique identifier of the modal to hide.
   *
   * @internal
   *
   * @since 0.0.1
   */
  public readonly hide = (id: string) => this.state.setState(produce(state => {
    if (!state[id]) throw new Error(`Modal with id: '${id}' does not exist`);
    state[id] = ModalState.Hidden({ props: {} });

    log.debug(`[ModalManager(${this.name})]: modal with id '${id}' has been closed.`);
  }));

  /**
   * Returns the `useModal` hook from the `modalContext` for accessing modal state and actions.
   *
   * @since 0.0.1
   */
  public readonly useModal = useModal;

  /**
   * Renders the modals managed by this manager.
   * @since 0.0.1
   */
  public readonly Provider = () => {
    const entries = this.state();

    return (
      <>
        {Object.entries(entries).map(([id, entry]) => {
          const Component = this.components[id];
          if (!Component) throw new Error(`Could not find component for modal with id: ${id}`);

          return (<modalContext.Provider key={id} value={{
            id,
            isVisible: entry._tag === "Visible",
            hide: () => this.hide(id)
          }}>
            <Component {...entry.props} />
          </modalContext.Provider>)
        })}
      </>
    )
  };
};
