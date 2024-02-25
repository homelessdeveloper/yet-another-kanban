import client from "../../axios-client";
import { useMutation } from "@tanstack/react-query";
import type { MoveAssignmentToMutationResponse, MoveAssignmentToPathParams } from "../types/MoveAssignmentTo";
import type { UseMutationOptions } from "@tanstack/react-query";

type MoveAssignmentToClient = typeof client<MoveAssignmentToMutationResponse, never, never>;
type MoveAssignmentTo = {
    data: MoveAssignmentToMutationResponse;
    error: never;
    request: never;
    pathParams: MoveAssignmentToPathParams;
    queryParams: never;
    headerParams: never;
    response: MoveAssignmentToMutationResponse;
    client: {
        parameters: Partial<Parameters<MoveAssignmentToClient>[0]>;
        return: Awaited<ReturnType<MoveAssignmentToClient>>;
    };
};
/**
     * @summary Used to change the position of assignments within the group.
     * @link /Api/Workspaces/:workspaceId/MoveAssignmentTo/:assignmentId/:groupId */
export function useMoveAssignmentTo(workspaceId: MoveAssignmentToPathParams["workspaceId"], assignmentId: MoveAssignmentToPathParams["assignmentId"], groupId: MoveAssignmentToPathParams["groupId"], options: {
    mutation?: UseMutationOptions<MoveAssignmentTo["response"], MoveAssignmentTo["error"], void>;
    client?: MoveAssignmentTo["client"]["parameters"];
} = {}) {
    const { mutation: mutationOptions, client: clientOptions = {} } = options ?? {};
    return useMutation({
        mutationFn: async () => {
            const res = await client<MoveAssignmentTo["data"], MoveAssignmentTo["error"], void>({
                method: "put",
                url: `/Api/Workspaces/${workspaceId}/MoveAssignmentTo/${assignmentId}/${groupId}`,
                ...clientOptions
            });
            return res.data;
        },
        ...mutationOptions
    });
}