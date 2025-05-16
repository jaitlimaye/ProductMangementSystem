import { Navigate } from "react-router";
import type {ReactNode} from "react";

const ProtectedRoute = ({ children, token }: { children: ReactNode, token:string | null }) => {
  return token ? children : <Navigate to="/login" replace />;
};

export default ProtectedRoute;