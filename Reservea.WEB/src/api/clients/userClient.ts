import { LoginRequest } from "../dtos/user/auth/loginRequest";
import { RegisterRequest } from "../dtos/user/auth/registerRequest";
import { LoginResponse } from "../dtos/user/auth/loginResponse";
import { UserForListResponse } from "../dtos/user/auth/userForLIstRensponse";
import {
  RoleResponse,
  UserDetailsResponse,
} from "../dtos/user/auth/userDetailsResponse";
import { apiClient } from "./apiClient";
import { UpdateUserRequest } from "../dtos/user/auth/updateUserRequest";

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
  await apiClient.post("api/user/Auth/register", data);
};

export const getUsersRequest = async (): Promise<
  Array<UserForListResponse>
> => {
  const response = await apiClient.get("api/user/Users");

  return response.data;
};

export const getUserDetailsRequest = async (
  userId: number
): Promise<UserDetailsResponse> => {
  const response = await apiClient.get("api/user/Users/" + userId);

  return response.data;
};

export const updateUserRequest = async (
  userId: number,
  request: UpdateUserRequest
) => {
  await apiClient.patch("api/user/Users/" + userId, request);
};

export const getAllRolesRequest = async (): Promise<Array<RoleResponse>> => {
  const result = await apiClient.get("api/user/Roles");

  return result.data;
};

export const deleteUserRequest = async (userId: number) => {
  await apiClient.delete("api/user/Users" + userId);
};

export const confirmEmailRequest = async (token: string, id: number) => {
  await apiClient.post("api/user/Auth/confirmEmail", { token, id });
};

export const sendResertPasswordEmailRequest = async (email: string) => {
  await apiClient.post("api/user/Auth/send-reset-password", { email });
};

export interface ResetPasswordRequest {
  token: string;
  newPassword: string;
  userId: number;
}

export const resetPassworrdRequest = async (request: ResetPasswordRequest) => {
  await apiClient.post("api/user/Auth/reset-password", request);
};
