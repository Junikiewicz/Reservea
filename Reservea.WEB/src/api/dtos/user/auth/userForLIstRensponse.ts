export interface UserForListResponse {
    id: number;
    email: string;
    isActive: boolean;
    roles: Array<number>;
}
