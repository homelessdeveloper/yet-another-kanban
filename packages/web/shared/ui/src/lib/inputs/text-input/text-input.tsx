import React, { InputHTMLAttributes } from 'react';
import { match } from 'ts-pattern';
import { cn } from '@yak/libs/cn';

/**
 * Props for the TextInput component.
 *
 * @since 0.0.1
 */
type InputProps = {
  /**
   * Whether the input has an error state.
   *
   * @since 0.0.1
   */
  error?: boolean;

  /**
   * The size of the input.
   *
   * @since 0.0.1
   */
  size?: 'base' | 'md';

  /**
   * The value of the input.
   *
   * @since 0.0.1
   */
  value?: string;

  /**
   * The label for the input.
   *
   * @since 0.0.1
   */
  label?: string;

  /**
   * Whether the input is required.
   *
   * @since 0.0.1
   */
  required?: boolean;

} & Omit<InputHTMLAttributes<HTMLInputElement>, 'size'>;

/**
 * Get the CSS classes for the input container based on its state.
 *
 * TODO: replace with class variance authority.
 *
 * @internal
 *
 * @since 0.0.1
 */
const getClasses = (
  args: { empty: boolean } & Required<
    Pick<InputProps, 'size' | 'disabled' | 'error'>
  >
) => {
  const { disabled, size, error, empty } = args;

  return {
    container: cn(
      'border-2 focus:outline-none rounded-sm',
      !disabled && [
        empty && 'bg-slate-50',
        !empty && 'bg-white',
        !error &&
        'border-slate-200 focus:shadow-text-input focus:border-indigo-600 hover:border-indigo-600',
        error &&
        'bg-white border-pink-700 text-red-700 focus:shadow-text-input-error hover:shadow-text-input-error',
      ],
      disabled && [
        'cursor-not-allowed',
        'bg-slate-100, border-slate-200 text-slate-300 focus:shadow-text-input hover:shadow-text-input',
      ],
      match(size)
        .with('md', () => 'p-4')
        .with('base', () => 'p-2.5')
        .exhaustive()
    ),
  };
};

/**
 * A text input component.
 *
 * @param props The props for the TextInput component.
 * @returns A React element representing the TextInput component.
 *
 * @since 0.0.1
 */
export const TextInput = React.forwardRef<HTMLInputElement, InputProps>(
  (
    {
      error = false,
      disabled = false,
      size = 'base',
      className = '',
      value,
      label,
      required,
      ...rest
    },
    ref
  ) => {
    const { container } = getClasses({
      error,
      disabled,
      size,
      empty: value !== undefined ? value.length === 0 : false,
    });
    return (
      <>
        {!label && (
          <input
            ref={ref}
            type="text"
            className={cn(container, className)}
            disabled={disabled}
            value={value}
            {...rest}
          />
        )}

        {label && (
          <div className={cn('flex flex-col', className)}>
            <label
              className={cn('text-sm mb-2 text-slate-500')}
              children={
                <>
                  {label} <span className="text-red-600" children="*" />
                </>
              }
            />
            <input
              ref={ref}
              type="text"
              className={container}
              disabled={disabled}
              value={value}
              {...rest}
            />
          </div>
        )}
      </>
    );
  }
);
