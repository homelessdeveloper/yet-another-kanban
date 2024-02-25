import { FC } from "react";
import { ROUTES } from "./routes";
import { RouteObject, useRoutes } from "react-router-dom"
import {
  HomePage,
  WorkspacePage,
  SignInPage,
  SignUpPage
} from "./ui"

/**
 * Route definitions for the router.
 *
 * @since 0.0.1
 */
const routes: Array<RouteObject> = [

  {
    path: ROUTES.Home.definition,
    element: <HomePage />
  },
  {
    path: ROUTES.Home.definition,
    element: <WorkspacePage />
  },

  {
    path: ROUTES.Workspace.definition,
    element: <WorkspacePage />
  },

  {
    path: ROUTES.Auth.SignIn.definition,
    element: <SignInPage />
  },

  {
    path: ROUTES.Auth.SignUp.definition,
    element: <SignUpPage />
  }

];

/**
 * The application router.
 *
 * @since 0.0.1
 */
export const Router: FC = () => {
  return useRoutes(routes);
}
