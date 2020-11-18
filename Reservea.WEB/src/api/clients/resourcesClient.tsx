import { ResourceForListResponse } from "../dtos/resources/resources/resourceForListResponse";

import { apiClient } from "./apiClient";

export const resourcesListRequest = async (): Promise<Array<ResourceForListResponse>> => {
  const response = await apiClient.get("/api/resources/Resources");
  
  return response.data;
};
