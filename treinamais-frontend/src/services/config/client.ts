import axios, { type AxiosInstance, AxiosHeaders } from "axios";

const API_BASE_URL =
  (import.meta as any)?.env?.VITE_API_URL?.toString() ?? "http://localhost:5056";

const api: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  timeout: 120000, // 2 minutes for Leonardo.AI operations
  headers: {
    Accept: "application/json",
  },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("auth_token");
  if (token) {
    const headers = AxiosHeaders.from(config.headers);
    headers.set("Authorization", `Bearer ${token}`);
    config.headers = headers;
  }
  const data = config.data as any;
  const isPlainObject = data && typeof data === "object" && !("append" in data) && !(data instanceof Blob);
  if (isPlainObject) {
    const headers = AxiosHeaders.from(config.headers);
    headers.set("Content-Type", "application/json");
    headers.set("Accept", headers.get("Accept") ?? "application/json");
    config.headers = headers;
  }
  return config;
});

api.interceptors.response.use(
  (resp) => resp,
  (error) => {
    if (error?.response?.status === 401) {
      localStorage.removeItem("auth_token");
      // Optional: redirect hint (actual redirect handled in app routes)
    }
    return Promise.reject(error);
  }
);
export default api;
