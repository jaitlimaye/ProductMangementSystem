export type registerRequest = {
    username: string;
    email: string;
    password: string;
    roleId: number; // Assuming roleId is a number
    confirmPassword?: string; // Optional for registration
}