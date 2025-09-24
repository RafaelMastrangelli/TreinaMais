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
export default api;
