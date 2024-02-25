import z from "zod"
import { FC } from "react";
import { Button, TextInput } from '@yak/web/shared/ui';
import { useForm } from 'react-hook-form';
import { Link, useNavigate } from "react-router-dom";
import { Password, Email } from "../../../types"
import { zodResolver } from "@hookform/resolvers/zod";
import { ROUTES } from "../../../routes";
import { ApiClient } from "@yak/web/shared/api";

/**
 * Schema representing the data for the sign-in form.
 *
 * @since 0.0.1
 */
export type SignInFormData = z.infer<typeof SignInFormData>
export const SignInFormData = z.object({
  email: Email,
  password: Password
});

/**
 * Component for the sign-in page.
 *
 * @since 0.0.1
 */
export const SignInPage: FC = () => {
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<SignInFormData>({
    mode: "all",
    resolver: zodResolver(SignInFormData)
  });

  const signIn = ApiClient.useSignIn({
    mutation: {
      onSuccess: data => {
        ApiClient.setToken(data.token);
        navigate(ROUTES.Home.getPath());
      }
    }
  });

  return (
    <div className="h-screen w-screen bg-blue-50 flex items-center justify-center">
      <div className="bg-white p-4 z-10 shadow-slate-600 drop-shadow-2xl">
        <div className="mb-4">
          <h1 className="text-2xl font-bold">Sign in</h1>
        </div>
        <form onSubmit={handleSubmit(data => signIn.mutate(data))}>
          <div className="flex flex-col mb-4">
            <TextInput
              label="Email"
              required
              error={errors.email != null}
              {...register('email')}
            />
          </div>
          {
            errors.email?.message &&
            <p className="text-red-600 m-2">{errors.email?.message}</p>
          }
          <div className="flex flex-col mb-4">
            <TextInput
              type="password"
              label="Password"
              required
              error={errors.password != null}
              {...register('password', { required: true })}
            />
            {
              errors.password?.message &&
              <p className="text-red-600 m-2">{errors.password?.message}</p>
            }
          </div>
          <div className="grid grid-cols-2 gap-3 justify-center">
            <div className="flex items-center justify-center">
              <div className="flex text-sm">
                <p className="text-sm inline-block mr-2"> Don't have an account? </p>
                <Link
                  to={ROUTES.Auth.SignUp.getPath()}
                  className="text-indigo-700 font-bold hover:underline "
                  children="Sign up"
                />
              </div>
            </div>
            <div className="flex items-center justify-end">
              <Button className="bg-indigo-700 hover:bg-indigo-600" type="submit">
                Sign in
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
};
