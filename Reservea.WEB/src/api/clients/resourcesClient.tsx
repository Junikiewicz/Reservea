import { ResourceForListResponse } from "../dtos/resources/resources/resourceForListResponse";
import { ResourceTypeForListResponse } from "../dtos/resources/resourceTypes/resourceTypeForListResponse";
import { AttributeForListResponse } from "../dtos/resources/attributes/attributeForListResponse";
import { ResourceDetailsResponse } from "../dtos/resources/resources/resourceDetailsResponse";
import { AddResourceResponse } from "../dtos/resources/resources/addResourceResponse";
import { ResourceTypeDetailsResponse } from "../dtos/resources/resourceTypes/resourceTypeDetailsResponse";
import { UpdateResourceTypeRequest } from "../dtos/resources/resourceTypes/updateResourceTypeRequest";
import { UpdateResourceRequest } from "../dtos/resources/resources/updateResourceRequest";
import { AddResourceTypeResponse } from "../dtos/resources/resourceTypes/addResourceTypeResponse";
import { apiClient } from "./apiClient";
import { ResourceAttributeResponse } from "../dtos/resources/resourceAttributes/resourceAttributeResponse";
import { ResourceTypeWithDetailsForListResponse } from "../dtos/resources/resourceTypes/resourceTypeWithDetailsForListResponse";
import { ResoucerTypeAvaliabilitiesResponse } from "../dtos/resources/resources/resoucerTypeAvaliabilitiesResponse";

export const resourcesListRequest = async (): Promise<
  Array<ResourceForListResponse>
> => {
  const response = await apiClient.get("/api/resources/Resources");

  return response.data;
};

export const resourcesTypeDetailsRequest = async (
  id: number
): Promise<ResourceTypeDetailsResponse> => {
  const response = await apiClient.get("/api/resources/ResourceTypes/" + id);

  return response.data;
};

export const resourcesTypesListRequest = async (): Promise<
  Array<ResourceTypeForListResponse>
> => {
  const response = await apiClient.get("/api/resources/ResourceTypes");

  return response.data;
};

export const resourcesTypesWithDetailsListRequest = async (): Promise<
  Array<ResourceTypeWithDetailsForListResponse>
> => {
  const response = await apiClient.get("/api/resources/ResourceTypes/details");

  return response.data;
};

export const resourcesTypeAttributesRequest = async (
  id: number
): Promise<Array<AttributeForListResponse>> => {
  const response = await apiClient.get(
    "/api/resources/ResourceTypes/" + id + "/attributes"
  );

  return response.data;
};

export const attributesListRequest = async (): Promise<
  Array<AttributeForListResponse>
> => {
  const response = await apiClient.get("/api/resources/Attributes");

  return response.data;
};

export const addAttributeRequest = async (
  attributeName: string
): Promise<AttributeForListResponse> => {
  const response = await apiClient.post("/api/resources/Attributes", {
    name: attributeName,
  });

  return response.data;
};

export const updateAttributeRequest = async (
  attributeId: number,
  attributeName: string
) => {
  await apiClient.put("/api/resources/Attributes/" + attributeId, {
    name: attributeName,
  });
};

export const deleteAttributeRequest = async (attributeId: number) => {
  await apiClient.delete("/api/resources/Attributes/" + attributeId);
};

export const resourceAttributesForTypeChangeRequest = async (
  resourceId: number,
  resourceTypeId: number
): Promise<Array<ResourceAttributeResponse>> => {
  const response = await apiClient.get(
    "/api/resources/Resources/" + resourceId + "/" + resourceTypeId
  );

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
  resourceData: any
) => {
  const requestData = JSON.parse(JSON.stringify(resourceData));

  for (const element of requestData.resourceAvailabilities) {
    let remaining = element.interval;
    let days = Math.floor(remaining / (60 * 24));
    remaining = remaining % (60 * 24);
    let hours = Math.floor(remaining / 60);
    remaining = remaining % 60;
    let minutes = remaining;
    element.interval = `${zeroPad(days, 2)}:${zeroPad(hours, 2)}:${zeroPad(
      minutes,
      2
    )}:00`;
  }

  await apiClient.put("/api/resources/Resources/" + resourceId, requestData);
};

export const updateResourceTypeRequest = async (
  resourceTypeId: number,
  resourceData: UpdateResourceTypeRequest
) => {
  await apiClient.put(
    "/api/resources/ResourceTypes/" + resourceTypeId,
    resourceData
  );
};

export const createResourceTypeRequest = async (
  resourceData: UpdateResourceTypeRequest
): Promise<AddResourceTypeResponse> => {
  const response = await apiClient.post(
    "/api/resources/ResourceTypes/",
    resourceData
  );

  return response.data;
};

const zeroPad = (num: number, places: number) =>
  String(num).padStart(places, "0");

export const createResourceRequest = async (
  resourceData: UpdateResourceRequest
): Promise<AddResourceResponse> => {
  for (const element of resourceData.resourceAvailabilities) {
    let remaining = element.interval;
    let days = Math.floor(remaining / (60 * 24));
    remaining = remaining % (60 * 24);
    let hours = Math.floor(remaining / 60);
    remaining = remaining % 60;
    let minutes = remaining;
    element.interval = `${zeroPad(days, 2)}:${zeroPad(hours, 2)}:${zeroPad(
      minutes,
      2
    )}:00`;
  }

  const response = await apiClient.post(
    "/api/resources/Resources/",
    resourceData
  );

  return response.data;
};

export const deleteResourceRequest = async (resourceId: Number) => {
  await apiClient.delete("/api/resources/Resources/" + resourceId);
};

export const deleteResourceTypeRequest = async (resourceTypeId: Number) => {
  await apiClient.delete("/api/resources/ResourceTypes/" + resourceTypeId);
};

export const resourceTypeAvaliabilitiesRequest = async (
  resourceTypeId: Number
): Promise<Array<ResoucerTypeAvaliabilitiesResponse>> => {
  const response = await apiClient.get<
    Promise<Array<ResoucerTypeAvaliabilitiesResponse>>
  >("/api/resources/Resources/availability/", {
    params: { resourceTypeId },
  });

  return response.data;
};
