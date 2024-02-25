import { type ClassValue, clsx } from "clsx";
import { twMerge } from "tailwind-merge";

/**
 * Combines multiple class names or class arrays into a single string.
 * Supports Tailwind CSS class names and arrays.
 * Supports overriding of Tailwind CSS class names.
 *
 * @param inputs - The class names or class arrays to combine.
 * @returns A string of combined class names.
 *
 * @since 0.0.1
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}
