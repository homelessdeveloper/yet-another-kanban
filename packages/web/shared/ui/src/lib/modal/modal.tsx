import { cn } from "@yak/libs/cn"
import { Dialog, Transition } from "@headlessui/react";
import {
  FC,
  ComponentProps,
  ComponentPropsWithRef,
  ComponentPropsWithoutRef,
  Fragment,
  HTMLAttributes,
  ReactElement,
  ReactNode,
  createContext,
  useContext
} from "react";

// ---------------------------------------------- //
// CONTEXT
// ---------------------------------------------- //

/**
 * Context value for the Modal component.
 */
type ModalContextValue = {
  /** Function to close the modal. */
  onClose: (_: boolean) => void,
  /** Whether the modal is currently shown. */
  show: boolean
};

/**
 * Context for the Modal component.
 */
const ModalContext = createContext<ModalContextValue | null>(null);

/**
 * Hook to use the Modal context.
 */
const useModalContext = () => {
  const context = useContext(ModalContext);

  if (!context) {
    throw new Error("Modal context is not defined. Did you forgot to wrap dependent components in <Modal/> ?");
  }

  return context;
}

// ---------------------------------------------- //
// TITLE
// ---------------------------------------------- //

/**
 * Props for the ModalTitle component.
 */
export type ModalTitleProps = ComponentProps<typeof Dialog.Title>

/**
 * Component for the title of a modal.
 */
const ModalTitle: FC<ModalTitleProps> = (props) => {
  const { className, ...rest } = props;

  return <Dialog.Title
    className={cn(
      "text-xl font-semibold text-gray-900 dark:text-white",
      className,
    )}
    {...rest}
  />
}

// ---------------------------------------------- //
// BUTTON
// ---------------------------------------------- //

/**
 * Props for the ModalCloseButton component.
 */
export type ModalCloseButtonProps = { children?: string } & Omit<HTMLAttributes<HTMLButtonElement>, "children">;

/**
 * Component for the close button of a modal.
 */
const ModalCloseButton: FC<ModalCloseButtonProps> = (props) => {
  const { onClose } = useModalContext();
  const { children = "Close Modal", className, ...rest } = props;

  return (
    <button
      className={cn(
        "text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900",
        "rounded-lg text-sm p-1.5 ml-auto inline-flex items-center",
        "dark:hover:bg-gray-600 dark:hover:text-white",
        className
      )}
      onClick={() => onClose(false)}
      {...rest}
    >
      <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fillRule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clipRule="evenodd"></path></svg>
      <span className="sr-only">children</span>
    </button>
  )
};

// ---------------------------------------------- //
// HEADER
// ---------------------------------------------- //

/**
 * Props for the ModalHeader component.
 */
export type ModalHeaderProps = HTMLAttributes<HTMLDivElement>;

/**
 * Component for the header of a modal.
 */
const ModalHeader: FC<ModalHeaderProps> = (props) => {
  const { className, ...rest } = props
  return (
    <header className={cn(
      "flex items-start justify-between p-4 border-b rounded-t dark:border-gray-600",
      className
    )}
      {...rest}
    />
  )
}

// ---------------------------------------------- //
// PANEL
// ---------------------------------------------- //

/**
 * Props for the ModalPanel component.
 */
export type ModalPanelProps = {
  children: ReactNode
} & Omit<ComponentProps<typeof Dialog.Panel>, "children">

/**
 * Component for the panel of a modal.
 */
const ModalPanel: FC<ModalPanelProps> = ({ ...props }) => {
  const { className, as = "div", children, ...rest } = props;

  return (
    <Transition.Child
      as={Fragment}
      enter="ease-out duration-300"
      enterFrom="opacity-0 scale-95"
      enterTo="opacity-100 scale-100"
      leave="ease-in duration-200"
      leaveFrom="opacity-100 scale-100"
      leaveTo="opacity-0 scale-95"
    >
      <Dialog.Panel
        as={as}
        className={cn(
          "w-full max-w-2xl max-h-full bg-white",
          className
        )}
        {...rest}
      >
        <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
          {children}
        </div>
      </Dialog.Panel>
    </Transition.Child>
  )
};

// ---------------------------------------------- //
// BODY
// ---------------------------------------------- //

/**
 * Props for the ModalBody component.
 */
export type ModalBodyProps = ComponentPropsWithoutRef<"div">;

/**
 * Component for the body of a modal.
 */
const ModalBody: FC<ModalDescriptionProps> = (props) => {
  const { className, ...rest } = props;

  {/* TODO: fix this later */ }
  {/* @ts-expect-error */ }
  return <div
    className={cn(
      "p-6 space-y-6",
      className
    )}
    {...rest}
  />
}

// ---------------------------------------------- //
// DESCRIPTION
// ---------------------------------------------- //

/**
 * Props for the ModalDescription component.
 */
export type ModalDescriptionProps = ComponentProps<typeof Dialog.Description>;

/**
 * Component for the description of a modal.
 */
const ModalDescription: FC<ModalDescriptionProps> = (props) => {
  return <Dialog.Description {...props} />
}


// ---------------------------------------------- //
// Footer
// ---------------------------------------------- //

/**
 * Props for the ModalFooter component.
 */
export type ModalFooterProps = HTMLAttributes<HTMLDivElement>

/**
 * Component for the footer of a modal.
 */
const ModalFooter: FC<ModalFooterProps> = (props) => {
  const { className, ...rest } = props;
  return (
    <footer
      className={cn(
        "flex items-center p-6 space-x-2 border-t",
        "border-gray-200 rounded-b dark:border-gray-600"
      )}
      {...rest}
    />

  )
}

// ---------------------------------------------- //
// MODAL
// ---------------------------------------------- //

/**
 * Props for the Modal component.
 */
export type ModalProps = {
  /** Whether the modal is shown. */
  show: boolean,
  /** The children of the modal. */
  children: ReactElement
} & Omit<ComponentPropsWithRef<typeof Dialog>, "children">

/**
 * Component for a modal dialog.
 */
export function Modal(props: ModalProps) {

  const {
    children,
    className,
    show,
    ...rest
  } = props;

  return (

    <ModalContext.Provider value={{ show, onClose: props.onClose }}>
      <Transition appear show={show} as={Fragment}>
        {/* TODO: fix this later */}
        {/* @ts-expect-error */}
        <Dialog
          open={show}
          className={cn(
            "fixed overflow-y-auto md:inset-0 h-[calc(100%-1rem)] max-h-full",
            "right-0 z-10 w-full p-4 overflow-x-hidden flex items-center justify-center",
            className
          )}
          {...rest}
        >

          <Transition.Child
            as={Fragment}
            enter="ease-out duration-300"
            enterFrom="opacity-0"
            enterTo="opacity-100"
            leave="ease-in duration-200"
            leaveFrom="opacity-100"
            leaveTo="opacity-0"
          >
            <div className="fixed inset-0 bg-black bg-opacity-25" />
          </Transition.Child>

          {children}
        </Dialog>
      </Transition>
    </ModalContext.Provider >
  );
};

Modal.CloseButton = ModalCloseButton
Modal.Title = ModalTitle;
Modal.Description = ModalDescription;
Modal.Header = ModalHeader;
Modal.Body = ModalBody;
Modal.Panel = ModalPanel;
Modal.Footer = ModalFooter;
