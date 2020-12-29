export interface UserDetailsResponse {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  isActive: boolean;
  roles: Array<any>;
}

export interface RoleResponse {
  id: number;
  name: string;
}
