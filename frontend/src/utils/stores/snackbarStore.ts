import { create } from 'zustand';

interface SnackbarState {
    message: string;
    isOpen: boolean;
    severity: 'success' | 'error' | 'warning' | 'info';
    showSnackbar: (message: string, severity: 'success' | 'error' | 'warning' | 'info') => void;
    closeSnackbar: () => void;
}

const useSnackbarStore = create<SnackbarState>((set) => ({
    message: '',
    isOpen: false,
    severity: 'info',
    showSnackbar: (message, severity) =>
        set({ message, severity, isOpen: true }),
    closeSnackbar: () =>
        set({ isOpen: false, message: '', severity: 'info' }),
}));

export default useSnackbarStore;