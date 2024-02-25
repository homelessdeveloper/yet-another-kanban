import type { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios'
import log from "loglevel";
import axios from 'axios'
import { StoreOps } from './store'
import { identity } from 'effect'
import { ApiClientEvent, EventsOps } from './events'

/**
 * Subset of AxiosRequestConfig
 *
 * @since 0.0.1
 */
export type RequestConfig<TData = unknown> = {
  baseURL?: string
  url?: string
  method: 'get' | 'put' | 'patch' | 'post' | 'delete'
  params?: unknown
  data?: TData
  responseType?: 'arraybuffer' | 'blob' | 'document' | 'json' | 'text' | 'stream'
  signal?: AbortSignal
  headers?: AxiosRequestConfig['headers']
}
/**
 * Subset of AxiosResponse
 *
 * @since 0.0.1
 */
export type ResponseConfig<TData = unknown> = {
  data: TData
  status: number
  statusText: string
  headers?: AxiosResponse['headers']
}

/**
 * Custom axios client instance
 *
 * @since 0.0.1
 */
export const axiosInstance = axios.create({
  // TODO: Make this configurable...
  baseURL: "/",
  headers: {}
})

/**
 * Interceptor for adding authorization token to outgoing requests.
 * Retrieves token from the store and attaches it to the request headers.
 *
 * @since 0.0.1
 */
axiosInstance.interceptors.request.use((config) => {
  const token = StoreOps.getToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

/**
 * Interceptor for handling JWT expiration errors.
 * Clears the expired token from the store and publishes a 'TokenExpiredEvent'.
 *
 * @since 0.0.1
 */
axiosInstance.interceptors.response.use(identity, (error: AxiosError) => {
  if (error?.response?.status === 401) {
    log.debug("[ApiClient]: JWT has expired.");
    StoreOps.setToken(null);

    log.debug("[ApiClient]: Publishing the 'TokenExpiredEvent'.");
    EventsOps.events$.next(ApiClientEvent.TokenExpired());
  }

  return Promise.reject(error);
});

/**
 * Kubb axios client
 *
 * @since 0.0.1
 */
export const axiosClient = async <TData, TError = unknown, TVariables = unknown>(config: RequestConfig<TVariables>): Promise<ResponseConfig<TData>> => {
  const promise = axiosInstance
    .request<TData, ResponseConfig<TData>>(config)
    .catch((e: AxiosError<TError>) => {
      throw e
    })

  return promise
}

export default axiosClient
