import axios from "axios";

export const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:8080/api",
  withCredentials: true
});

export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
}

export async function login(email: string, password: string) {
  const response = await api.post<AuthTokens>("/auth/login", { email, password });
  return response.data;
}
