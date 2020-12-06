import {ResourceTypeAttributeResponse} from "../../../dtos/resources/resourceTypeAttributes/resourceTypeAttributeResponse";

export interface UpdateResourceTypeRequest {
    name: string;
    description: string;
    resourceTypeAttributes: Array<ResourceTypeAttributeResponse>;
}
