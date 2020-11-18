import { LoginRequest } from "../dtos/user/auth/loginRequest";
import { RegisterRequest } from "../dtos/user/auth/registerRequest";
import { LoginResponse } from "../dtos/user/auth/loginResponse";

import { apiClient } from "./apiClient";

export const loginRequest = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  const response = await apiClient.post<LoginResponse>(
    "/api/user/Auth/login",
    data
  );
  return response.data;
};

export const signupRequest = async (data: RegisterRequest) => {
  await apiClient.post("​/api​/user​s/Auth​/register", data);
};
