"use client";

import { create } from "zustand";

interface AuthState {
  accessToken?: string;
  refreshToken?: string;
  setTokens: (accessToken: string, refreshToken: string) => void;
  clear: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  setTokens: (accessToken, refreshToken) => set({ accessToken, refreshToken }),
  clear: () => set({ accessToken: undefined, refreshToken: undefined })
}));
