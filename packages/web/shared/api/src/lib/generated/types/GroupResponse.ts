import type { AssignmentResponse } from "./AssignmentResponse";

export type GroupResponse = {
    /**
     * @type string uuid
    */
    id: string;
    /**
     * @type string
    */
    name: string;
    /**
     * @type string uuid
    */
    workspaceId: string;
    /**
     * @type integer int32
    */
    position: number;
    /**
     * @type array
    */
    assignments: AssignmentResponse[];
};