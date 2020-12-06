import { HttpStatusType } from "../../common/enums/HttpStatusType";
import { getUserToken } from "../../common/helpers/localStorageHelper";
import axios, { AxiosRequestConfig, AxiosError, AxiosResponse } from "axios";
import http from "http";
import https from "https";
import { REQUEST_START_TIME_HEADER } from "../../common/variables/headers";
import { toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBug, faUserSlash } from "@fortawesome/free-solid-svg-icons";
import React from "react";

const apiUrl = (window as any)._env_.API_BASE_URL;

const httpAgent = new http.Agent({ keepAlive: true });
const httpsAgent = new https.Agent({ keepAlive: true });

export const apiClient = axios.create({
  baseURL: apiUrl,
  validateStatus: (status: number) => status === HttpStatusType.OK || status === HttpStatusType.NoContent,
  httpAgent,
  httpsAgent,
});

apiClient.interceptors.request.use((config: AxiosRequestConfig) => {
  const tokenResponse = getUserToken();
  if (tokenResponse) config.headers.Authorization = `Bearer ${tokenResponse}`;
  config.headers[REQUEST_START_TIME_HEADER] = new Date().getTime();
  return config;
});

apiClient.interceptors.response.use(
  (value: AxiosResponse<unknown>) => {
    return value;
  },
  (err: AxiosError) => {
    const status = err.response?.status;
    if (status === undefined) {
      toast.error("Błąd połączenia");
      return Promise.reject();
    }
    if (status === 401) {
      toast.error(() => <div><FontAwesomeIcon className="mx-2" size="lg" icon={faUserSlash}/>Błąd autoryzacji</div> );
      localStorage.removeItem("token");
      return Promise.reject();
    }
    if (status === 403) {
      toast.error(`Niewystarczające upraweniania`);
      return Promise.reject();
    }
    if (status >= 500) {
      toast.error(() => <div><FontAwesomeIcon className="mx-2" size="lg" icon={faBug}/>Coś poszło nie tak</div> );
      return Promise.reject(err);
    }
    if (axios.isCancel(err)) {
      return Promise.reject(err);
    }
    return Promise.reject(err);
  }
);
