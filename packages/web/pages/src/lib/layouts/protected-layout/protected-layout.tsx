import log from "loglevel";
import { AxiosError, isAxiosError } from "axios"
import { ApiClient } from "@yak/web/shared/api";
import { FC, PropsWithChildren, useEffect } from "react";
import { useNavigate } from "react-router-dom";

/**
 * Props for the protected layout component.
 *
 * @since 0.0.1
 */
export type ProtectedLayoutProps = PropsWithChildren<{ redirectTo: string }>;

/**
 * Layout component that protects pages from unauthenticated access.
 *
 * @since 0.0.1
 */
export const ProtectedLayout: FC<ProtectedLayoutProps> = (props) => {
  const { children, redirectTo } = props;
  const navigate = useNavigate();
  const getPrincipal = ApiClient.useGetPrincipal();

  useEffect(() => {
    if (isAxiosError(getPrincipal.error)) {
      const error = getPrincipal.error as AxiosError<any, any>

      if (error.status === 401) {
        log.debug(`[<ProtectedLayout/>]: User is not logged-in. Redirecting to '${redirectTo}' route...`)
        navigate(redirectTo);
      }
    }
  }, [getPrincipal.error]);

  return <>{children}</>;
};
