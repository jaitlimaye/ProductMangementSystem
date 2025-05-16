import {create} from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import {jwtDecode} from 'jwt-decode';
export type IAuth  = {
  token: string | null;
  roles: string[];
  expiresAt: Date | null;
  setToken: (token: string | null) => void;
  logout: () => void;
}

export type JwtPayload = {
  username: string;
  email: string;
  roles: string[];
  exp: number;
  iss: string;
  aud: string;
}

const useAuthStore = create<IAuth>()(
  persist(
    (set) => ({
      token: null,
      roles: [],
      expiresAt: null,
      setToken: (token) => {
        // Save raw token
        if (!token) {
          set({ token: null, roles: [], expiresAt: null });
        }
        else {
        localStorage.setItem("access_token", token);
    
        // decode
        const { roles, exp } = jwtDecode<JwtPayload>(token);
    
        // compute expiration date
        const expiresAt = new Date(exp * 1000);
    
        set({ token, roles, expiresAt });
        }
      },
      logout: () => {
        localStorage.removeItem("access_token");
        set({ token: null, roles: [], expiresAt: null });
      },
    }),
    
    {
      name: 'auth-storage', 
      storage : createJSONStorage(() => localStorage),
    }
  )
);

export default useAuthStore;
