export type AssignmentResponse = {
    /**
     * @type string uuid
    */
    id: string;
    /**
     * @type string
    */
    title: string;
    /**
     * @type string | undefined
    */
    description?: string | null;
    /**
     * @type integer int32
    */
    position: number;
};