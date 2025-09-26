import axios, { AxiosError } from "axios";

export class ApiError<T = unknown> extends Error {
  status?: number;
  data?: T;
  constructor(message: string, status?: number, data?: T) {
    super(message);
    this.name = "ApiError";
    this.status = status;
    this.data = data;
  }
}

export function toApiError(err: unknown): ApiError {
  if (axios.isAxiosError(err)) {
    const e = err as AxiosError<any>;
    return new ApiError(
      e.message || "Falha na requisição",
      e.response?.status,
      e.response?.data
    );
  }
  if (err instanceof ApiError) return err; // Evita recursão infinita
  if (err instanceof Error) return new ApiError(err.message);
  return new ApiError("Erro desconhecido");
}
