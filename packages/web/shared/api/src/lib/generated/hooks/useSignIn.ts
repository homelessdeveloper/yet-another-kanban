import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { SignInMutationRequest, SignInMutationResponse } from "../types/SignIn";
import type { UseMutationOptions } from "@tanstack/react-query";

type SignInClient = typeof client<SignInMutationResponse, never, SignInMutationRequest>;
type SignIn = {
    data: SignInMutationResponse;
    error: never;
    request: SignInMutationRequest;
    pathParams: never;
    queryParams: never;
    headerParams: never;
    response: SignInMutationResponse;
    client: {
        parameters: Partial<Parameters<SignInClient>[0]>;
        return: Awaited<ReturnType<SignInClient>>;
    };
};
/**
     * @summary Authenticates existing user.
     * @link /Api/Auth/SignIn */
export function useSignIn(options: {
    mutation?: UseMutationOptions<SignIn["response"], SignIn["error"], SignIn["request"]>;
    client?: SignIn["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<SignIn["data"], SignIn["error"], SignIn["request"]>({
                method: "post",
                url: `/Api/Auth/SignIn`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}