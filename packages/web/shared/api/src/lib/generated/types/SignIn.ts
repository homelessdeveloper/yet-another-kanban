import type { AuthResponse } from "./AuthResponse";
import type { SignInRequest } from "./SignInRequest";

export type SignIn200 = AuthResponse;

 export type SignInMutationRequest = SignInRequest;

 export type SignInMutationResponse = AuthResponse;
export type SignInMutation = {
    Response: SignInMutationResponse;
    Request: SignInMutationRequest;
};