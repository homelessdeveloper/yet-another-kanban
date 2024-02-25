import { Popover } from "@headlessui/react";
import { ComponentProps, FC, PropsWithChildren } from "react";
import { useFloating, shift } from "@floating-ui/react"
import { cn } from "@yak/libs/cn";

/**
 * Props for the FabItem component.
 *
 * @since 0.0.1
 */
export type FabItemProps = ComponentProps<"button">

/**
 * A floating action button item component.
 *
 * @param props The props for the FabItem component.
 * @returns A React element representing the FabItem component.
 *
 * @since 0.0.1
 */
const FabItem: FC<FabItemProps> = (props) => (
  <button
    className={cn(
      "flex items-center justify-center",
      'w-16 h-16 rounded-full bg-white drop-shadow-xl',
      'text-slate-800 text-3xl font-bold mb-4',
    )}
    {...props}
  />
)

/**
 * Props for the Fab component.
 *
 * @since 0.0.1
 */
type FabProps = PropsWithChildren<{}>

/**
 * A floating action button component.
 *
 * @param props The props for the Fab component.
 * @returns A React element representing the Fab component.
 *
 * @since 0.0.1
 */
export const Fab = (props: FabProps) => {
  const { children } = props;
  const { refs, floatingStyles } = useFloating({
    placement: "top",
    middleware: [shift()]
  });

  return (
    <Popover>
      <Popover.Button
        className={cn(
          'w-16 h-16 rounded-full bg-indigo-600 drop-shadow-xl',
          'text-white text-3xl font-bold',
        )}
        ref={refs.setReference}
        children="+"
      />
      <Popover.Panel
        ref={refs.setFloating}
        as="div"
        className="flex flex-col"
        children={children}
        style={floatingStyles}
      />
    </Popover>
  );
};

// Assign the FabItem component to the Fab component for easier access.
Fab.Item = FabItem;
