import { Data } from "effect";

/**
 * Represents the state of a modal, either hidden or visible with props.
 *
 * @since 0.0.1
 */
export type ModalState = Data.TaggedEnum<{
  Hidden: { props: {} },
  Visible: { props: any }
}>;

/**
 * Utility factory to create a tagged enum for `ModalState`.
 *
 * @since 0.0.1
 */
export const ModalState = Data.taggedEnum<ModalState>();
