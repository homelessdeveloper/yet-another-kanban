/**
 * Declarations of the routes.
 * This code useful as it gives compile-time check of the routes.
 *
 * @example
 * ```tsx
 *  export const MyComponent = () => {
 *    return (
 *    <div>
 *     <Link to={ROUTES.Auth.GetPath()}
 *    </div>
 *    )
 *  };
 * ```
 *
 * @since 0.0.1
 */
export const ROUTES = {
  Home: {
    definition: "/",
    getPath: () => "/",
  },

  Workspace: {
    definition: "/:workspaceId",
    getPath: (workspaceId: string) => `/workspaces/${workspaceId}`
  },

  Auth: {
    SignIn: {
      definition: "/auth/sign-in",
      getPath: () => `/auth/sign-in`
    },

    SignUp: {
      definition: "/auth/sign-up",
      getPath: () => `/auth/sign-up`
    }
  }
}

