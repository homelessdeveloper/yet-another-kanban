import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { MoveAssignmentOverMutationResponse, MoveAssignmentOverPathParams } from "../types/MoveAssignmentOver";
import type { UseMutationOptions } from "@tanstack/react-query";

type MoveAssignmentOverClient = typeof client<MoveAssignmentOverMutationResponse, never, never>;
type MoveAssignmentOver = {
    data: MoveAssignmentOverMutationResponse;
    error: never;
    request: never;
    pathParams: MoveAssignmentOverPathParams;
    queryParams: never;
    headerParams: never;
    response: MoveAssignmentOverMutationResponse;
    client: {
        parameters: Partial<Parameters<MoveAssignmentOverClient>[0]>;
        return: Awaited<ReturnType<MoveAssignmentOverClient>>;
    };
};
/**
     * @summary Changes the position of assignments within the group.
     * @link /Api/Workspaces/:workspaceId/MoveAssignmentOver/:activeAssignmentId/:overAssignmentId */
export function useMoveAssignmentOver(workspaceId: MoveAssignmentOverPathParams["workspaceId"], activeAssignmentId: MoveAssignmentOverPathParams["activeAssignmentId"], overAssignmentId: MoveAssignmentOverPathParams["overAssignmentId"], options: {
    mutation?: UseMutationOptions<MoveAssignmentOver["response"], MoveAssignmentOver["error"], void>;
    client?: MoveAssignmentOver["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<MoveAssignmentOver["data"], MoveAssignmentOver["error"], void>({
                method: "put",
                url: `/Api/Workspaces/${workspaceId}/MoveAssignmentOver/${activeAssignmentId}/${overAssignmentId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}