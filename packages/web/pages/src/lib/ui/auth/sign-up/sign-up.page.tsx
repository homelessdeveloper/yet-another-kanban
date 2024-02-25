import z from "zod"
import { Button, TextInput } from "@yak/web/shared/ui";
import { FC } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { ROUTES } from "../../../routes";
import { ApiClient } from "@yak/web/shared/api";
import {
  Email,
  UserName,
  Password
} from "../../../types";

/**
 * Represents the data schema for the sign-up form.
 *
 * @since 0.0.1
 */
export type SignUpFormData = z.infer<typeof SignUpFormData>
export const SignUpFormData = z.object({
  email: Email,
  userName: UserName,
  password: Password
});


/**
 * SignUpPage component displays a sign-up form for users to register.
 * It uses React Hook Form for form validation and submission.
 *
 * @since 0.0.1
 */
export const SignUpPage: FC = () => {
  const navigation = useNavigate();

  const {
    handleSubmit,
    register,
    formState: { errors }
  } = useForm<SignUpFormData>({
    mode: "all",
    resolver: zodResolver(SignUpFormData)
  });

  const signUp = ApiClient.useSignUp({
    mutation: {
      onSuccess(result) {
        ApiClient.setToken(result.token);
        navigation(ROUTES.Home.getPath())
      }
    }
  });

  return (
    <div className="h-screen w-screen bg-blue-50 flex items-center justify-center">
      {/* FORM */}
      <div className="bg-white p-4 z-10 shadow-slate-600 drop-shadow-2xl">
        <div className="mb-4">
          <h1 className="text-2xl font-bold">Sign up</h1>
        </div>
        <form onSubmit={handleSubmit(data => signUp.mutate(data))}>
          <div className="grid grid-cols-2 gap-4">
            <div className="mb-4">
              <TextInput
                label="Email"
                error={errors.email != null}
                required
                {...register("email")}
              />
              {errors.email?.message && <p className="text-red-600 mt-2">{errors.email.message}</p>}
            </div>
            <div className="flex flex-col mb-4">
              <TextInput
                label="Username"
                error={errors.userName != null}
                {...register("userName")}
              />
              {errors.userName?.message && <p className="text-red-600 mt-2">{errors.userName.message}</p>}
            </div>
          </div>
          <div className="flex flex-col mb-4">
            <TextInput
              type="password"
              label="Password"
              error={errors.password != null}
              {...register("password")}
            />
            {errors.password?.message && <p className="text-red-600 mt-2">{errors.password.message}</p>}
          </div>
          <div className="grid grid-cols-2 gap-3 justify-center">
            <div className="flex items-center justify-center">
              <div className="flex text-sm">
                <p className="text-sm inline-block mr-2"> Don't have an account? </p>
                <Link
                  to={ROUTES.Auth.SignIn.getPath()}
                  className="text-indigo-700 font-bold hover:underline "
                  children="Sign in"
                />
              </div>
            </div>
            <div className="flex items-center justify-end">
              <Button type="submit" className="bg-indigo-700 hover:bg-indigo-600">Sign up</Button>
            </div>
          </div>
        </form>
      </div>
      {/* END OF FORM */}
    </div>
  );
};
