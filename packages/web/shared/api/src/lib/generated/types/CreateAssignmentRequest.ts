export type CreateAssignmentRequest = {
    /**
     * @type string
    */
    title: string;
    /**
     * @type string uuid
    */
    groupId: string;
    /**
     * @type string | undefined
    */
    description?: string | null;
};