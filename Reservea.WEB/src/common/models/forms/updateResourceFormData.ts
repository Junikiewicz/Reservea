import { ResourceAttributeResponse } from "../../../api/dtos/resources/resourceAttributes/resourceAttributeResponse";
import { ResoucerTypeAvaliabilitiesResponse } from "../../../api/dtos/resources/resources/resoucerTypeAvaliabilitiesResponse";

export interface UpdateResourceFormData {
  name: string;
  description: string;
  pricePerHour: number;
  resourceTypeId: number;
  resourceStatusId: number;
  resourceAttributes: Array<ResourceAttributeResponse>;
  resourceAvailabilities: Array<ResoucerTypeAvaliabilitiesResponse>;
}
