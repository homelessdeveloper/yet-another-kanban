import { ComponentProps, FC, Fragment, createContext, useContext } from "react";
import { cva, VariantProps } from 'class-variance-authority';
import { cn } from "@yak/libs/cn"
import { Menu, Transition } from '@headlessui/react'
import { UseFloatingReturn, shift, useFloating } from "@floating-ui/react"

// ----------------------------------------------------------- //
// Context
// ----------------------------------------------------------- //

/**
 * Context for the ContextMenu component.
 *
 * @since 0.0.1
 */
export type ContextMenuContext = UseFloatingReturn

const ContextMenuContext = createContext<ContextMenuContext | null>(null);

/**
 * Hook to access the ContextMenuContext.
 * Throws an error if the context is not found.
 *
 * @since 0.0.1
 */
const useMenuContext = () => {
  const ctx = useContext(ContextMenuContext);

  if (!ctx) {
    throw new Error("Could not find ContextMenuContext. Did you forget to wrap menu components in <ContextMenu/>")
  }

  return ctx;
}


// ----------------------------------------------------------- //
// COMPONENTS
// ----------------------------------------------------------- //

/**
 * Class variance for styling different context menu items.
 */
const contextMenuItemClasses = cva(
  cn(
    "p-2 space-x-1.5 flex text-slate-600",
    "items-center cursor-pointer rounded-md",
    "font-medium"
  ),
  {
    variants: {
      color: {
        slate: "bg-white  hover:bg-slate-100 hover:text-slate-900",
        pink: "bg-white hover:bg-pink-100 hover:text-pink-700",
        red: "bg-white hover:bg-red-100 hover:text-red-700",
        blue: "bg-white hover:bg-blue-100 hover:text-blue-700",
        green: "bg-white hover:bg-green-100 hover:text-green-700",
        yellow: "bg-white hover:bg-yellow-100 hover:text-yellow-700",
        indigo: "bg-white hover:bg-indigo-100 hover:text-indigo-700",
      },
    }
  })

/**
 * Props for the ContextMenuItem component.
 */
export type ContextMenuItemProps = Omit<ComponentProps<typeof Menu.Item>, "color">
  & VariantProps<typeof contextMenuItemClasses>

/**
 * A context menu item component.
 *
 * @param props The props for the ContextMenuItem component.
 * @returns A React element representing the ContextMenuItem component.
 */
const ContextMenuItem: FC<ContextMenuItemProps> = (props) => {

  const {
    className,
    color = "slate",
    ...rest
  } = props;

  return (
    <Menu.Item
      {...rest}
      as={"button"}
      className={cn(
        contextMenuItemClasses({ className, color }),
        className
      )}
    />
  )
};

/**
 * Props for the ContextMenuDivider component.
 */
export type ContextMenuDividerProps = ComponentProps<"hr">;

/**
 * A divider component for the context menu.
 *
 * @param props The props for the ContextMenuDivider component.
 * @returns A React element representing the ContextMenuDivider component.
 */
const ContextMenuDivider: FC<ContextMenuDividerProps> = (props) => {
  const { className, ...rest } = props;

  return (
    <hr
      {...rest}
      className={cn("w-full bg-gray-600", className)}
    />
  )
}

/**
 * Props for the ContextMenuButton component.
 */
export type ContextMenuButtonProps = ComponentProps<typeof Menu.Button>;

/**
 * A button component for the context menu.
 *
 * @param props The props for the ContextMenuButton component.
 * @returns A React element representing the ContextMenuButton component.
 */
const ContextMenuButton: FC<ContextMenuButtonProps> = (props) => {
  const { refs } = useMenuContext();

  return <Menu.Button
    ref={refs.setReference}
    {...props}
  />

}

/**
 * Props for the ContextMenuItems component.
 */
export type ContextMenuItemsProps = ComponentProps<typeof Menu.Items>;

/**
 * A component for the context menu items.
 *
 * @param props The props for the ContextMenuItems component.
 * @returns A React element representing the ContextMenuItems component.
 */
const ContextMenuItems: FC<ContextMenuItemsProps> = (props) => {
  const { className, ...rest } = props;
  const { refs, floatingStyles } = useMenuContext();
  return (
    <Transition
      as={Fragment}
      enter="transition ease-out duration-100"
      enterFrom="transform opacity-0 scale-95"
      enterTo="transform opacity-100 scale-100"
      leave="transition ease-in duration-75"
      leaveFrom="transform opacity-100 scale-100"
      leaveTo="transform opacity-0 scale-95"

    >
      <Menu.Items
        {...rest}
        ref={refs.setFloating}
        className={cn(
          "p-2 rounded-full shadow-xl rounded-lg bg-white text-sm",
          "flex flex-col space-y-1",
          className,
        )}
        style={floatingStyles}
      />
    </Transition>
  )
};

/**
 * Props for the ContextMenu component.
 */
export type ContextMenuProps = ComponentProps<typeof Menu> & {
  // floatingUI?: Partial<UseFloatingOptions>
}

/**
 * A context menu component.
 *
 * @param props The props for the ContextMenu component.
 * @returns A React element representing the ContextMenu component.
 */
export const ContextMenu: FC<ContextMenuProps> & {
  Button: FC<ContextMenuButtonProps>
  Items: FC<ContextMenuItemsProps>
  Item: FC<ContextMenuItemProps>
  Divider: FC<ContextMenuDividerProps>
} = (props) => {

  const floatingCtx = useFloating({
    middleware: [shift()]
  });

  return (
    <ContextMenuContext.Provider
      value={floatingCtx}
      children={<Menu {...props} />}
    />
  );
};

// Assign sub-components to the ContextMenu component for easier access.
ContextMenu.Button = ContextMenuButton;
ContextMenu.Items = ContextMenuItems;
ContextMenu.Item = ContextMenuItem;
ContextMenu.Divider = ContextMenuDivider;
