import { FC, ComponentProps } from "react"
import { cn } from "@yak/libs/cn";

/**
 * Props for the NumberBadge component.
 *
 * @since 0.0.1
 */
type NumberBadgeProps = {
  /**
   * The number to display inside the badge.
   *
   * @since 0.0.1
   * */
  number: number
} & Omit<ComponentProps<"span">, "children">

/**
 * A badge component that displays a number inside a rounded badge.
 *
 * @param props The props for the NumberBadge component.
 * @returns A React element representing the NumberBadge component.
 *
 * @since 0.0.1
 */
export const NumberBadge: FC<NumberBadgeProps> = (props) => {
  const {
    className,
    number,
    ...rest
  } = props;

  return (
    <span
      className={cn(
        "flex items-center justify-center",
        "text-blue-500 bg-white rounded bg-opacity-30",
        "w-5 h-5 ml-2 text-sm font-semibold",
        className
      )}
      {...rest}
    >
      {number}
    </span>
  );
}
