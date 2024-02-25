import { ModalManager } from "./modal-manager";

/**
 * Represents a model for managing a specific modal within a `ModalManager`.
 *
 * @typeParam Props - The type of props accepted by the modal component.
 *
 * @since 0.0.1
 */
export class ModalModel<Props extends object = {}> {

  /**
   * Creates an instance of `ModalModel`.
   *
   * @param id - The unique identifier for the modal.
   * @param manager - The `ModalManager` instance managing this modal.
   *
   * @since 0.0.1
   */
  public constructor(
    public readonly id: string,
    private readonly manager: ModalManager
  ) { }

  /**
   * Shows the modal with the specified props.
   *
   * @param props - The props to pass to the modal component.
   *
   * @since 0.0.1
   */
  public readonly show = (props: Props): void => {
    this.manager.show(this.id, props);
  }

  /**
   * Hides the modal.
   *
   * @since 0.0.1
   */
  public readonly hide = (): void => {
    this.manager.hide(this.id);
  }
}
