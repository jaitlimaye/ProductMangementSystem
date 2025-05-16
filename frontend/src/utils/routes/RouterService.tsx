import {Routes, Route, BrowserRouter } from "react-router";
import { LoginPage,RegisterPage,Home } from "../../pages";
import ProtectedRoute from "./ProtectedRoute";
import useAuthStore from "../stores/authStore";
import type {IAuth } from "../stores/authStore";
import { Navigate } from "react-router";

export const RouterService = () => {
    const token = useAuthStore((state : IAuth) => state.token);
    return (
      <BrowserRouter>
        <Routes>
            <Route
            path="/"
        element={<Navigate to={token ? "/home" : "/login"} replace />}
      />
      <Route
        path="/login"
        element={token ? <Navigate to="/home" replace /> : <LoginPage />}
      />
      <Route
        path="/register"
        element={token ? <Navigate to="/home" replace /> : <RegisterPage />}
      />
      <Route
        path="/home"
        element={
          <ProtectedRoute token={token}>
            <Home />
          </ProtectedRoute>
        }
      />
      

      <Route path="*" element={<Navigate to={token ? "/home" : "/login"} replace />} />
        </Routes>
        </BrowserRouter>
    )
}