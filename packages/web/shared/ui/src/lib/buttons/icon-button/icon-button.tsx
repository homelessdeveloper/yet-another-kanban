import { FC, ComponentProps } from "react";
import { cn } from "@yak/libs/cn";

/**
 * Props for the IconButton component.
 *
 * @since 0.0.1
 */
export type IconButtonProps = Omit<ComponentProps<"button">, "children"> & {
  /**
   * The icon element to display inside the button.
   *
   * @since 0.0.1
   */
  icon: JSX.Element,
}

/**
 * A button component that displays an icon.
 *
 * @param props The props for the IconButton component.
 * @returns A React element representing the IconButton component.
 * @since 0.0.1
 */
export const IconButton: FC<IconButtonProps> = (props) => {
  const { icon, className, ...rest } = props;

  return (
    <button
      {...rest}
      className={cn(
        "text-blue-500 rounded ",
        "hover:bg-indigo-500 hover:text-indigo-100",
        "w-6 h-6 inline-flex items-center justify-center",
        className,
      )}
      children={icon}
    />
  );
}
