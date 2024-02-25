import { ApiClient } from "@yak/web/shared/api";
import { useQuery } from "@tanstack/react-query";
import { QueryConfig } from "@yak/libs/react-query";
import { MODULE_NAME } from "../../../../constants";

/**
 * Returns the query key for listing workspaces.
 *
 * @since 0.0.1
 */
export const getUseListWorkspacesQueryKey = () => [`[${MODULE_NAME}]#listWorkspaces`] as const;

/**
 * Configuration for the useListWorkspaces hook.
 *
 * @since 0.0.1
 */
export type UseListWorkspacesQueryConfig = QueryConfig<typeof ApiClient.listWorkspaces>;


/**
 * A React hook for listing workspaces.
 *
 * @param {UseListWorkspacesQueryConfig} config - The configuration for the query.
 * @since 0.0.1
 */
export const useListWorkspaces = (config?: UseListWorkspacesQueryConfig) => {
  return useQuery({
    ...config,
    queryKey: getUseListWorkspacesQueryKey(),
    queryFn: ApiClient.listWorkspaces
  });
};
