export interface UpdateUserRequest {
    email: string;
    firstName: string;
    lastName: string;
    roles: Array<string>;
}