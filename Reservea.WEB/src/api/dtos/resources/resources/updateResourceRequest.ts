import { ResourceAttributeResponse } from "../resourceAttributes/resourceAttributeResponse";
import { ResoucerTypeAvaliabilitiesResponse } from "./resoucerTypeAvaliabilitiesResponse";

export interface UpdateResourceRequest {
  name: string;
  description: string;
  pricePerHour: number;
  resourceTypeId: number;
  resourceStatusId: number;
  resourceAttributes: Array<ResourceAttributeResponse>;
  resourceAvailabilities: Array<ResoucerTypeAvaliabilitiesResponse>;
}
