import { FC } from "react";
import { cn } from "@yak/libs/cn";
import { cva, VariantProps } from "class-variance-authority";

/**
 * @internal
 *
 * @since 0.0.1
 */
const pillClasses = cva(
  cn(
    "inline-flex items-center h-6 px-3",
    "text-xs rounded-full font-semibold"
  ),
  {
    variants: {
      color: {
        pink: "text-pink-500 bg-pink-100",
        yellow: "text-yellow-500 bg-yellow-100",
        green: "text-green-500 bg-green-100"
      }
    }
  }
);

/**
 * Define props for TextPill component
 *
 * @since 0.0.1
 */
export type TextPillProps = VariantProps<typeof pillClasses> & {
  text: string;
  className?: string;
};

/**
 * TextPill component displays a pill-shaped text with a specified color.
 *
 * @since 0.0.1
 */
export const TextPill: FC<TextPillProps> = ({ className, text, color = "pink" }) => {
  return (
    <span className={cn(pillClasses({ className, color }), className)}>
      {text}
    </span>
  );
};
