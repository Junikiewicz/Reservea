import { ResourceAttributeResponse } from "../resourceAttributes/resourceAttributeResponse";
import { ResoucerTypeAvaliabilitiesResponse } from "./resoucerTypeAvaliabilitiesResponse";

export interface ResourceDetailsResponse {
  id: number;
  name: string;
  description: string;
  pricePerHour: number;
  resourceStatusId: number;
  resourceTypeId: number;
  resourceAttributes: Array<ResourceAttributeResponse>;
  resourceAvailabilities: Array<ResoucerTypeAvaliabilitiesResponse>;
}
