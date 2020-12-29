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
