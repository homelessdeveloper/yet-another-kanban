import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { GetWorkspaceByIdQueryResponse, GetWorkspaceByIdPathParams } from "../../types/GetWorkspaceById";

/**
     * @description Deletes existing workspace and all groups and tasks within it.
     * @summary Retrieves workspace by its ID
     * @link /Api/Workspaces/:workspaceId */
export async function getWorkspaceById(workspaceId: GetWorkspaceByIdPathParams["workspaceId"], options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<GetWorkspaceByIdQueryResponse>["data"]> {
    const res = await client<GetWorkspaceByIdQueryResponse>({
        method: "get",
        url: `/Api/Workspaces/${workspaceId}`,
        ...options
    });
    return res.data;
}