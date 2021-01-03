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
