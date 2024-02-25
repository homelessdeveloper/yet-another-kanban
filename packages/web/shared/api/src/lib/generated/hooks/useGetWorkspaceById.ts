import { getWorkspaceByIdQueryResponseSchema } from "../zod/getWorkspaceByIdSchema";
import client from "../../axios-client";
import { useQuery, queryOptions } from "@tanstack/react-query";
import type { GetWorkspaceByIdQueryResponse, GetWorkspaceByIdPathParams } from "../types/GetWorkspaceById";
import type { QueryObserverOptions, UseQueryResult, QueryKey } from "@tanstack/react-query";

type GetWorkspaceByIdClient = typeof client<GetWorkspaceByIdQueryResponse, never, never>;
type GetWorkspaceById = {
    data: GetWorkspaceByIdQueryResponse;
    error: never;
    request: never;
    pathParams: GetWorkspaceByIdPathParams;
    queryParams: never;
    headerParams: never;
    response: GetWorkspaceByIdQueryResponse;
    client: {
        parameters: Partial<Parameters<GetWorkspaceByIdClient>[0]>;
        return: Awaited<ReturnType<GetWorkspaceByIdClient>>;
    };
};
export const getWorkspaceByIdQueryKey = (workspaceId: GetWorkspaceByIdPathParams["workspaceId"]) => [{ url: "/Api/Workspaces/:workspaceId", params: { workspaceId: workspaceId } }] as const;
export type GetWorkspaceByIdQueryKey = ReturnType<typeof getWorkspaceByIdQueryKey>;
export function getWorkspaceByIdQueryOptions(workspaceId: GetWorkspaceByIdPathParams["workspaceId"], options: GetWorkspaceById["client"]["parameters"] = {}) {
    const queryKey = getWorkspaceByIdQueryKey(workspaceId);
    return queryOptions({
        queryKey,
        queryFn: async () => {
            const res = await client<GetWorkspaceById["data"], GetWorkspaceById["error"]>({
                method: "get",
                url: `/Api/Workspaces/${workspaceId}`,
                ...options
            });
            return getWorkspaceByIdQueryResponseSchema.parse(res.data);
        },
    });
}
/**
     * @description Deletes existing workspace and all groups and tasks within it.
     * @summary Retrieves workspace by its ID
     * @link /Api/Workspaces/:workspaceId */
export function useGetWorkspaceById<TData = GetWorkspaceById["response"], TQueryData = GetWorkspaceById["response"], TQueryKey extends QueryKey = GetWorkspaceByIdQueryKey>(workspaceId: GetWorkspaceByIdPathParams["workspaceId"], options: {
    query?: Partial<QueryObserverOptions<GetWorkspaceById["response"], GetWorkspaceById["error"], TData, TQueryData, TQueryKey>>;
    client?: GetWorkspaceById["client"]["parameters"];
} = {}): UseQueryResult<TData, GetWorkspaceById["error"]> & {
    queryKey: TQueryKey;
} {
    const { query: queryOptions, client: clientOptions = {} } = options ?? {};
    const queryKey = queryOptions?.queryKey ?? getWorkspaceByIdQueryKey(workspaceId);
    const query = useQuery({
        ...getWorkspaceByIdQueryOptions(workspaceId, clientOptions) as QueryObserverOptions,
        queryKey,
        ...queryOptions as unknown as Omit<QueryObserverOptions, "queryKey">
    }) as UseQueryResult<TData, GetWorkspaceById["error"]> & {
        queryKey: TQueryKey;
    };
    query.queryKey = queryKey as TQueryKey;
    return query;
}