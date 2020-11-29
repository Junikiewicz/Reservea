import { ResourceForListResponse } from "../dtos/resources/resources/resourceForListResponse";
import { ResourceTypeForListResponse } from "../dtos/resources/resourceTypes/resourceTypeForListResponse";
import { AttributeForListResponse } from "../dtos/resources/attributes/attributeForListResponse";
import { ResourceDetailsResponse } from "../dtos/resources/resources/resourceDetailsResponse";
import { UpdateResourceRequest } from "../dtos/resources/resources/updateResourceRequest";

import { apiClient } from "./apiClient";
import { ResourceAttributeResponse } from "../dtos/resources/resourceAttributes/resourceAttributeResponse";

export const resourcesListRequest = async (): Promise<
  Array<ResourceForListResponse>
> => {
  const response = await apiClient.get("/api/resources/Resources");

  return response.data;
};

export const resourcesTypesListRequest = async (): Promise<
  Array<ResourceTypeForListResponse>
> => {
  const response = await apiClient.get("/api/resources/ResourceTypes");

  return response.data;
};

export const attributesListRequest = async (): Promise<
  Array<AttributeForListResponse>
> => {
  const response = await apiClient.get("/api/resources/Attributes");

  return response.data;
};

export const resourceAttributesForTypeChangeRequest = async (resourceId: number,
  resourceTypeId: number,): Promise<
  Array<ResourceAttributeResponse>
> => {
  const response = await apiClient.get("/api/resources/Resources/"+resourceId+"/"+ resourceTypeId);

  return response.data;
};


export const resourceDetailsRequest = async (
  resourceId: number
): Promise<ResourceDetailsResponse> => {
  const response = await apiClient.get(
    "/api/resources/Resources/" + resourceId
  );

  return response.data;
};

export const updateResourceRequest = async (
  resourceId: number,
  resourceData: UpdateResourceRequest
) => {
  await apiClient.put(
    "/api/resources/Resources/" + resourceId,
    resourceData
  );
};
