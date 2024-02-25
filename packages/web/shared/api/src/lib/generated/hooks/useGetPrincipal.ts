import { getPrincipalQueryResponseSchema } from "../zod/getPrincipalSchema";
import client from "../../axios-client";
import { useQuery, queryOptions } from "@tanstack/react-query";
import type { GetPrincipalQueryResponse } from "../types/GetPrincipal";
import type { QueryObserverOptions, UseQueryResult, QueryKey } from "@tanstack/react-query";

type GetPrincipalClient = typeof client<GetPrincipalQueryResponse, never, never>;
type GetPrincipal = {
    data: GetPrincipalQueryResponse;
    error: never;
    request: never;
    pathParams: never;
    queryParams: never;
    headerParams: never;
    response: GetPrincipalQueryResponse;
    client: {
        parameters: Partial<Parameters<GetPrincipalClient>[0]>;
        return: Awaited<ReturnType<GetPrincipalClient>>;
    };
};
export const getPrincipalQueryKey = () => [{ url: "/Api/Auth/GetPrincipal" }] as const;
export type GetPrincipalQueryKey = ReturnType<typeof getPrincipalQueryKey>;
export function getPrincipalQueryOptions(options: GetPrincipal["client"]["parameters"] = {}) {
    const queryKey = getPrincipalQueryKey();
    return queryOptions({
        queryKey,
        queryFn: async () => {
            const res = await client<GetPrincipal["data"], GetPrincipal["error"]>({
                method: "get",
                url: `/Api/Auth/GetPrincipal`,
                ...options
            });
            return getPrincipalQueryResponseSchema.parse(res.data);
        },
    });
}
/**
     * @summary Retrieves currently authenticated user
     * @link /Api/Auth/GetPrincipal */
export function useGetPrincipal<TData = GetPrincipal["response"], TQueryData = GetPrincipal["response"], TQueryKey extends QueryKey = GetPrincipalQueryKey>(options: {
    query?: Partial<QueryObserverOptions<GetPrincipal["response"], GetPrincipal["error"], TData, TQueryData, TQueryKey>>;
    client?: GetPrincipal["client"]["parameters"];
} = {}): UseQueryResult<TData, GetPrincipal["error"]> & {
    queryKey: TQueryKey;
} {
    const { query: queryOptions, client: clientOptions = {} } = options ?? {};
    const queryKey = queryOptions?.queryKey ?? getPrincipalQueryKey();
    const query = useQuery({
        ...getPrincipalQueryOptions(clientOptions) as QueryObserverOptions,
        queryKey,
        ...queryOptions as unknown as Omit<QueryObserverOptions, "queryKey">
    }) as UseQueryResult<TData, GetPrincipal["error"]> & {
        queryKey: TQueryKey;
    };
    query.queryKey = queryKey as TQueryKey;
    return query;
}
