import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { SignUpMutationRequest, SignUpMutationResponse } from "../../types/SignUp";

/**
     * @summary Registers and authenticates new user
     * @link /Api/Auth/SignUp */
export async function signUp(data?: SignUpMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<SignUpMutationResponse>["data"]> {
    const res = await client<SignUpMutationResponse, SignUpMutationRequest>({
        method: "post",
        url: `/Api/Auth/SignUp`,
        data,
        ...options
    });
    return res.data;
}