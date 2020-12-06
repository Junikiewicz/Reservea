import { ResourceTypeAttributeResponse } from "../resourceTypeAttributes/resourceTypeAttributeResponse";

export interface ResourceTypeDetailsResponse {
    id: number;
    name: string;
    description: string;
    resourceTypeAttributes: Array<ResourceTypeAttributeResponse>;
    isDeleted: boolean;
}
