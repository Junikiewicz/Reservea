import { ResourceTypeAttributeResponse } from "../../../api/dtos/resources/resourceTypeAttributes/resourceTypeAttributeResponse";

export interface UpdateResourceTypeFormData {
    name: string;
    description: string;
    resourceTypeAttributes: Array<ResourceTypeAttributeResponse>;
}
