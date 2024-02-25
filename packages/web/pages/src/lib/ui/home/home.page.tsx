import { FC } from "react";
import { DefaultLayout, ProtectedLayout } from "../../layouts";
import { ROUTES } from "../../routes";

/**
 * The default page where no workspace is selected.
 *
 * @since 0.0.1
 */
export const HomePage: FC = () => {
  return (

    <ProtectedLayout redirectTo={ROUTES.Auth.SignIn.getPath()}>
      <DefaultLayout>
        <div className="w-full min-h-screen flex items-center justify-center">
          <span>
            Create workspace or choose existing!
          </span>
        </div>
      </DefaultLayout>
    </ProtectedLayout>
  )
};
