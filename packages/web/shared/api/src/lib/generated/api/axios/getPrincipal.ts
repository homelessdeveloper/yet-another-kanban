import client from "../../../axios-client";
import type { ResponseConfig } from "../../../axios-client";
import type { GetPrincipalQueryResponse } from "../../types/GetPrincipal";

/**
     * @summary Retrieves currently authenticated user
     * @link /Api/Auth/GetPrincipal */
export async function getPrincipal(options: Partial<Parameters<typeof client>[0]> = {}): Promise<ResponseConfig<GetPrincipalQueryResponse>["data"]> {
    const res = await client<GetPrincipalQueryResponse>({
        method: "get",
        url: `/Api/Auth/GetPrincipal`,
        ...options
    });
    return res.data;
}