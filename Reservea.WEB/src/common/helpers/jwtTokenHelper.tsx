import jwt_decode from "jwt-decode";

import { getUserToken, removeUserToken } from "../../common/helpers/localStorageHelper";

var decodedToken: any;

export const getUserNameFromJWTToken = (): string =>{
    if(!decodedToken) setJWTToken();
    
    return decodedToken.unique_name;
}

export const checkIfValidToken = (): boolean => {
    if(!decodedToken) setJWTToken();

    if(Date.now() >= decodedToken.exp * 1000)
    {
        removeUserToken();
        return false;
    } 
    
    return true;
};

const setJWTToken = () =>{
    var userToken = getUserToken();
    decodedToken = decodeJWTToken(userToken);
}

const decodeJWTToken = (token: string): any => {
    return jwt_decode(token);
};
