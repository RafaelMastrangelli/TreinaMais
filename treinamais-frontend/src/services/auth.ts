import api from "./config/client";

export type LoginPayload = { username: string; password: string };
export type LoginResponse = { token: string };

export async function login(payload: LoginPayload): Promise<void> {
  const { data } = await api.post<LoginResponse>("/api/auth/login", payload);
  localStorage.setItem("auth_token", data.token);
}

export function logout(): void {
  localStorage.removeItem("auth_token");
}

export function isAuthenticated(): boolean {
  return Boolean(localStorage.getItem("auth_token"));
}


