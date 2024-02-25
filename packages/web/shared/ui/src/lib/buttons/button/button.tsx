import { FC, ComponentProps } from 'react';
import { cn } from '@yak/libs/cn';
import { VariantProps, cva } from "class-variance-authority";

/**
 * Button variants available for styling.
 *
 * @since 0.0.1
 */
const buttonVariants = cva("", {
  variants: {
    variant: {
      /**
       * Primary button variant.
       */
      primary: cn(
        "text-white bg-blue-700 hover:bg-blue-800 focus:ring-4",
        "focus:outline-none focus:ring-blue-300 font-medium rounded-lg",
        "text-sm px-5 py-2.5 text-center dark:bg-blue-600",
        "dark:hover:bg-blue-700 dark:focus:ring-blue-800",
      ),
      /**
       * Secondary button variant.
       */
      secondary: cn(
        'focus:ring-4 focus:outline-none focus:ring-blue-300',
        'text-gray-500 bg-white hover:bg-gray-100',
        'rounded-lg border border-gray-200 text-sm font-medium',
        'px-5 py-2.5 hover:text-gray-900 focus:z-10',
        'dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500',
        'dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600',
      ),
    }
  }
})

/**
 * Props for the Button component, including variant options.
 *
 * @since 0.0.1
 */
export type ButtonProps = ComponentProps<"button"> & VariantProps<typeof buttonVariants>

/**
 * Button component that can be styled with different variants.
 *
 * @param props The props for the Button component.
 * @returns A React element representing the Button component.
 *
 * @since 0.0.1
 */
export const Button: FC<ButtonProps> = (props) => {
  const {
    className,
    variant = "primary",
    ...rest } = props;
  return (
    <button
      className={cn(buttonVariants({ className, variant }), className)}
      {...rest}
    />
  );
};
