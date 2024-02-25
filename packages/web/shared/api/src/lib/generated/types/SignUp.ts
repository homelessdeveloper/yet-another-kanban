import type { AuthResponse } from "./AuthResponse";
import type { SignUpRequest } from "./SignUpRequest";

export type SignUp200 = AuthResponse;

 export type SignUpMutationRequest = SignUpRequest;

 export type SignUpMutationResponse = AuthResponse;
export type SignUpMutation = {
    Response: SignUpMutationResponse;
    Request: SignUpMutationRequest;
};