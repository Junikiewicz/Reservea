const TOKEN_KEY = `USER_TOKEN`;

export const getUserToken = (): string => {
    const serializedToken =  window.localStorage.getItem(TOKEN_KEY);
    return serializedToken ? JSON.parse(serializedToken) : null;
};

export const setUserToken = (token: string): void => {
    window.localStorage.setItem(TOKEN_KEY, JSON.stringify(token));
};

export const removeUserToken = (): void => {
    window.localStorage.removeItem(TOKEN_KEY);
};
