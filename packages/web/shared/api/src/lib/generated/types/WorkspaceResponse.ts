import type { GroupResponse } from "./GroupResponse";

export type WorkspaceResponse = {
    /**
     * @type string uuid
    */
    id: string;
    /**
     * @type string
    */
    name: string;
    /**
     * @type array
    */
    groups: GroupResponse[];
};