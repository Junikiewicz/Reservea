import { ResourceAttributeResponse } from "../../../api/dtos/resources/resourceAttributes/resourceAttributeResponse";

export interface UpdateResourceFormData {
    name: string;
    description: string;
    pricePerHour: number;
    resourceTypeId: number;
    resourceStatusId: number;
    resourceAttributes: Array<ResourceAttributeResponse>;
}
