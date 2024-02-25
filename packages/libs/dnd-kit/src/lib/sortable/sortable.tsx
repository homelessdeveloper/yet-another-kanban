import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { ComponentProps, FC, ReactNode } from "react";

/**
 * Represents the state of a sortable element.
 *
 * @since 0.0.1
 */
type SortableState = ReturnType<typeof useSortable> & {
  /** The style object for the sortable element. */
  style: {
    transform?: string,
    transition?: string
  }
}

/**
 * Props for the Sortable component.
 *
 * @since 0.0.1
 */
export type SortableProps = Omit<ComponentProps<"div">, "className" | "children"> & {
  /** The class name or function returning class names for the sortable element. */
  className?: string | ((state: SortableState) => string)
  /** The children element or function returning children for the sortable element. */
  children?: ReactNode | ((state: SortableState) => ReactNode)
  /** The options for the useSortable hook. */
  options: Parameters<typeof useSortable>[0]
};

/**
 * Sortable component for creating sortable elements.
 *
 * @param props - The component props.
 * @returns The rendered Sortable component.
 *
 * @since 0.0.1
 */
export const Sortable: FC<SortableProps> = (props) => {
  const {
    className,
    options,
    children,
    ...rest
  } = props;

  const sortable = useSortable(options);
  const style = {
    transform: CSS.Translate.toString(sortable.transform),
    transition: sortable.transition,
  }

  const state: SortableState = { ...sortable, style };

  return (
    <div
      {...sortable.listeners}
      {...sortable.attributes}
      {...rest}
      style={style}
      ref={sortable.setNodeRef}
      className={typeof className === "function" ? className(state) : className}
      children={typeof children === "function" ? children(state) : children}
    />
  )
};
