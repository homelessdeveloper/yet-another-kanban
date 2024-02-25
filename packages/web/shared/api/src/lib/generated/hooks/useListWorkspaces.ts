import { listWorkspacesQueryResponseSchema } from "../zod/listWorkspacesSchema";
import client from "../../axios-client";
import { useQuery, queryOptions } from "@tanstack/react-query";
import type { ListWorkspacesQueryResponse } from "../types/ListWorkspaces";
import type { QueryObserverOptions, UseQueryResult, QueryKey } from "@tanstack/react-query";

type ListWorkspacesClient = typeof client<ListWorkspacesQueryResponse, never, never>;
type ListWorkspaces = {
    data: ListWorkspacesQueryResponse;
    error: never;
    request: never;
    pathParams: never;
    queryParams: never;
    headerParams: never;
    response: ListWorkspacesQueryResponse;
    client: {
        parameters: Partial<Parameters<ListWorkspacesClient>[0]>;
        return: Awaited<ReturnType<ListWorkspacesClient>>;
    };
};
export const listWorkspacesQueryKey = () => [{ url: "/Api/Workspaces" }] as const;
export type ListWorkspacesQueryKey = ReturnType<typeof listWorkspacesQueryKey>;
export function listWorkspacesQueryOptions(options: ListWorkspaces["client"]["parameters"] = {}) {
    const queryKey = listWorkspacesQueryKey();
    return queryOptions({
        queryKey,
        queryFn: async () => {
            const res = await client<ListWorkspaces["data"], ListWorkspaces["error"]>({
                method: "get",
                url: `/Api/Workspaces`,
                ...options
            });
            return listWorkspacesQueryResponseSchema.parse(res.data);
        },
    });
}
/**
     * @summary Lists all workspaces that user owns
     * @link /Api/Workspaces */
export function useListWorkspaces<TData = ListWorkspaces["response"], TQueryData = ListWorkspaces["response"], TQueryKey extends QueryKey = ListWorkspacesQueryKey>(options: {
    query?: Partial<QueryObserverOptions<ListWorkspaces["response"], ListWorkspaces["error"], TData, TQueryData, TQueryKey>>;
    client?: ListWorkspaces["client"]["parameters"];
} = {}): UseQueryResult<TData, ListWorkspaces["error"]> & {
    queryKey: TQueryKey;
} {
    const { query: queryOptions, client: clientOptions = {} } = options ?? {};
    const queryKey = queryOptions?.queryKey ?? listWorkspacesQueryKey();
    const query = useQuery({
        ...listWorkspacesQueryOptions(clientOptions) as QueryObserverOptions,
        queryKey,
        ...queryOptions as unknown as Omit<QueryObserverOptions, "queryKey">
    }) as UseQueryResult<TData, ListWorkspaces["error"]> & {
        queryKey: TQueryKey;
    };
    query.queryKey = queryKey as TQueryKey;
    return query;
}