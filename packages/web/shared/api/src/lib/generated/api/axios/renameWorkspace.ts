import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { RenameWorkspaceMutationRequest, RenameWorkspaceMutationResponse, RenameWorkspacePathParams } from "../../types/RenameWorkspace";

/**
     * @summary Rename workspace by id
     * @link /Api/Workspaces/:workspaceId */
export async function renameWorkspace(workspaceId: RenameWorkspacePathParams["workspaceId"], data?: RenameWorkspaceMutationRequest, options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<RenameWorkspaceMutationResponse>["data"]> {
    const res = await client<RenameWorkspaceMutationResponse, RenameWorkspaceMutationRequest>({
        method: "post",
        url: `/Api/Workspaces/${workspaceId}`,
        data,
        ...options
    });
    return res.data;
}