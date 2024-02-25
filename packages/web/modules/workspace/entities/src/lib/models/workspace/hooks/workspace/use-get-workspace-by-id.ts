import { ApiClient } from "@yak/web/shared/api";
import { useQuery } from "@tanstack/react-query";
import { QueryConfig } from "@yak/libs/react-query";
import { MODULE_NAME } from "../../../../constants";

/**
 * Returns the query key for retrieving a workspace by ID.
 *
 * @param {string} id - The ID of the workspace.
 *
 * @since 0.0.1
 */
export const getUseGetWorkspaceByIdQueryKey = (id: string) => [`[${MODULE_NAME}]#getWorkspaceById(${id})`];

/**
 * Configuration for the useGetWorkspaceById hook.
 *
 * @since 0.0.1
 */
export type UseGetWorkspaceByIdQueryConfig = QueryConfig<typeof ApiClient.getWorkspaceById>;

/**
 * A React hook for retrieving a workspace by ID.
 *
 * @param {string} id - The ID of the workspace to retrieve.
 * @param {UseGetWorkspaceByIdQueryConfig} config - The configuration for the query.
 *
 * @since 0.0.1
 */
export const useGetWorkspaceById = (id: string, config: UseGetWorkspaceByIdQueryConfig = {}) => {
  return useQuery({
    queryFn: () => ApiClient.getWorkspaceById(id),
    queryKey: getUseGetWorkspaceByIdQueryKey(id),
    ...config,
  });
};
