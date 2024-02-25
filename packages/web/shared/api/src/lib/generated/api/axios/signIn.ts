import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { SignInMutationRequest, SignInMutationResponse } from "../../types/SignIn";

/**
     * @summary Authenticates existing user.
     * @link /Api/Auth/SignIn */
export async function signIn(data?: SignInMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<SignInMutationResponse>["data"]> {
    const res = await client<SignInMutationResponse, SignInMutationRequest>({
        method: "post",
        url: `/Api/Auth/SignIn`,
        data,
        ...options
    });
    return res.data;
}