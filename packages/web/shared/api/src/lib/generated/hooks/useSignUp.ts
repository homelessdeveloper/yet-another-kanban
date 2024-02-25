import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { SignUpMutationRequest, SignUpMutationResponse } from "../types/SignUp";
import type { UseMutationOptions } from "@tanstack/react-query";

type SignUpClient = typeof client<SignUpMutationResponse, never, SignUpMutationRequest>;
type SignUp = {
    data: SignUpMutationResponse;
    error: never;
    request: SignUpMutationRequest;
    pathParams: never;
    queryParams: never;
    headerParams: never;
    response: SignUpMutationResponse;
    client: {
        parameters: Partial<Parameters<SignUpClient>[0]>;
        return: Awaited<ReturnType<SignUpClient>>;
    };
};
/**
     * @summary Registers and authenticates new user
     * @link /Api/Auth/SignUp */
export function useSignUp(options: {
    mutation?: UseMutationOptions<SignUp["response"], SignUp["error"], SignUp["request"]>;
    client?: SignUp["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async (data) => {
            const res = await client<SignUp["data"], SignUp["error"], SignUp["request"]>({
                method: "post",
                url: `/Api/Auth/SignUp`,
                data,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}