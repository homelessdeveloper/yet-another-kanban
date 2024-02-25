import { AxiosError } from 'axios';
import { AsyncReturnType, Promisable } from 'type-fest';
import { UseQueryOptions, UseMutationOptions } from '@tanstack/react-query';


/**
 * QueryConfig defines configuration options for a query function.
 * @template QueryFnType - The type of the query function.
 *
 * @since 0.0.1
 */
export type QueryConfig<QueryFnType extends (...args: any) => any> = Omit<
  UseQueryOptions<AsyncReturnType<QueryFnType>>,
  'queryKey' | 'queryFn'
>;




/**
 * MutationConfig defines configuration options for a mutation function.
 * @template MutationFnType - The type of the mutation function.
 *
 * @since 0.0.1
 */
export type MutationConfig<MutationFnType extends (...args: any) => any> = UseMutationOptions<
  AsyncReturnType<MutationFnType>,
  AxiosError, // The error type expected from Axios
  Parameters<MutationFnType>[0] // The parameter type of the mutation function
>;
