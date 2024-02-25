import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { MoveGroupOverMutationResponse, MoveGroupOverPathParams } from "../types/MoveGroupOver";
import type { UseMutationOptions } from "@tanstack/react-query";

type MoveGroupOverClient = typeof client<MoveGroupOverMutationResponse, never, never>;
type MoveGroupOver = {
    data: MoveGroupOverMutationResponse;
    error: never;
    request: never;
    pathParams: MoveGroupOverPathParams;
    queryParams: never;
    headerParams: never;
    response: MoveGroupOverMutationResponse;
    client: {
        parameters: Partial<Parameters<MoveGroupOverClient>[0]>;
        return: Awaited<ReturnType<MoveGroupOverClient>>;
    };
};
/**
     * @summary Used to swap the position of two groups within a workspace.
     * @link /Api/Workspaces/:workspaceId/MoveGroupOver/:activeGroupId/:overGroupId */
export function useMoveGroupOver(workspaceId: MoveGroupOverPathParams["workspaceId"], activeGroupId: MoveGroupOverPathParams["activeGroupId"], overGroupId: MoveGroupOverPathParams["overGroupId"], options: {
    mutation?: UseMutationOptions<MoveGroupOver["response"], MoveGroupOver["error"], void>;
    client?: MoveGroupOver["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<MoveGroupOver["data"], MoveGroupOver["error"], void>({
                method: "patch",
                url: `/Api/Workspaces/${workspaceId}/MoveGroupOver/${activeGroupId}/${overGroupId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}