import { Data } from "effect";
import * as API from "@yak/web/shared/api";

/**
 * Represents the different types of items that can be dragged.
 *
 * @since 0.0.1
 */
export type DraggedItem = Data.TaggedEnum<{
  /**
   * Represents a state where no item is being dragged.
   *
   * @since 0.0.1
   */
  None: {};

  /**
   * Represents a state where a group is being dragged.
   *
   * @since 0.0.1
   */
  Group: API.GroupResponse; // Represents a group being dragged

  /**
   * Represents a state where an assignment is being dragged.
   *
   * @since 0.0.1
   */
  Assignment: API.AssignmentResponse; // Represents an assignment being dragged
}>;

/**
 * A dictionary of constructors for items being dragged
 *
 * @since 0.0.1
 */
export const DraggedItem = Data.taggedEnum<DraggedItem>();
