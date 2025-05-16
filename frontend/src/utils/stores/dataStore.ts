import {create} from 'zustand';
import { type User, type Category, type Product } from '../types/data/datatype';

export interface dataState {
    
    userlistdata : User[];
    setUserListData: (data: User[] ) => void;
    productlistdata : Product[] ;
    setProductListData: (data:  Product[] ) => void;
    productFilter: number | null;
    setProductFilter: (data: number | null) => void;
    categorylistdata : Category[] ;
    setCategoryListData: (data:  Category[]  ) => void;
}

const useDataStore = create<dataState>((set) => ({
    userlistdata : [],
    setUserListData: (data) => set({ userlistdata: data }),
    productlistdata : [],
    setProductListData: (data) => set({ productlistdata: data }),
    productFilter: null,
    setProductFilter: (data) => set({ productFilter: data }),
    categorylistdata : [],
    setCategoryListData: (data) => set({categorylistdata: data })
}));

export default useDataStore;