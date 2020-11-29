import { ResourceAttributeResponse } from "../resourceAttributes/resourceAttributeResponse";

export interface UpdateResourceRequest {
    name: string;
    description: string;
    pricePerHour: number;
    resourceTypeId: number;
    resourceStatusId: number;
    resourceAttributes: Array<ResourceAttributeResponse>;
}
