import { ResourceAttributeResponse } from "../resourceAttributes/resourceAttributeResponse";

export interface ResourceDetailsResponse {
    id: number;
    name: string;
    description: string;
    pricePerHour: number;
    resourceStatusId: number;
    resourceTypeId: number;
    resourceAttributes: Array<ResourceAttributeResponse>;
}
