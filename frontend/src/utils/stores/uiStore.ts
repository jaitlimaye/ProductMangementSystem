import {create} from 'zustand';

export interface UIState {
    listdisplay : string;
    setListDisplay: (display: string) => void;
    loading: boolean;
    setLoading: (loading: boolean) => void;
    isModalOpen: boolean;
    setModalOpen: (isOpen: boolean) => void;
    modalType: string;
    setModalType: (type: string) => void;
}

const useUIStore = create<UIState>((set) => ({
    listdisplay : 'Products',
    setListDisplay: (display) => set({ listdisplay: display}),
    loading: false,
    setLoading: (loading) => set({ loading: loading}),
    isModalOpen: false,
    setModalOpen: (isOpen) => set({ isModalOpen: isOpen}),
    modalType: 'create',
    setModalType: (type) => set({ modalType: type })
}));

export default useUIStore;