import { ResourceForListResponse } from "../dtos/resources/resources/resourceForListResponse";
import { ResourceTypeForListResponse } from "../dtos/resources/resourceTypes/resourceTypeForListResponse";

import { apiClient } from "./apiClient";

export const resourcesListRequest = async (): Promise<Array<ResourceForListResponse>> => {
  const response = await apiClient.get("/api/resources/Resources");
  
  return response.data;
};

export const resourcesTypesListRequest = async (): Promise<Array<ResourceTypeForListResponse>> => {
    const response = await apiClient.get("/api/resources/ResourceTypes");
    
    return response.data;
  };
