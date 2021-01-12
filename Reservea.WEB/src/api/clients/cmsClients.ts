import { apiClient } from "./apiClient";

export interface TextFieldContent {
  id: number;
  name: string;
  content: string;
}

export const getTextFieldContentRequest = async (
  name: string
): Promise<TextFieldContent> => {
  const response = await apiClient.get("api/cms/HomePage/" + name);

  return response.data;
};

export const updateTextFieldContentsRequest = async (
  data: Array<TextFieldContent>
) => {
  await apiClient.patch("api/cms/HomePage", data);
};

export const getAllTextFieldsContentsRequest = async (): Promise<
  Array<TextFieldContent>
> => {
  const result = await apiClient.get("api/cms/HomePage/");

  return result.data;
};

export interface Photo {
  Id: number;
  Url: string;
  PublicId: string;
}

export const uploadImage = async (file: any): Promise<Photo> => {
  var formData = new FormData();
  formData.append("File", file);

  const response = await apiClient.post("api/cms/photos", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  return response.data;
};

export const getAllImages = async (): Promise<Array<Photo>> => {
  const result = await apiClient.get("api/cms/photos");

  return result.data;
};

export const getImage = async (id: number): Promise<Photo> => {
  const result = await apiClient.get("api/cms/photos/" + id);

  return result.data;
};

export const deleteImage = async (id: number) => {
  await apiClient.delete("api/cms/photos/" + id);
};

export const addUserRateRequest = async (request: any) => {
  await apiClient.post("api/cms/UserRates", request);
};

export const updateUserRatesRequest = async (request: Array<any>) => {
  await apiClient.patch("api/cms/UserRates", request);
};

export const getAllUserRatesRequest = async () => {
  const response = await apiClient.get("api/cms/UserRates");

  return response.data;
};

export const getUserRatesForHomepageRequest = async () => {
  const response = await apiClient.get("api/cms/UserRates/homepage");

  return response.data;
};
