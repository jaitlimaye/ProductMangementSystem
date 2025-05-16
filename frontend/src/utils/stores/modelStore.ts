import {create} from 'zustand';
import type { Category, Product } from '../types';

export interface modelState {
    productFormData : Product;
    setProductFormData: (data:  Product) => void;
    categoryFormData : Category;
    setCategoryFormData: (data:  Category) => void;
}

const useModelStore = create<modelState>((set) => ({
    productFormData : {
        productId: 0,
        name: "",
        description: "",
        price: 0,
        categoryId: undefined,
        imageUrl: undefined,
      },
      setProductFormData: (data) => set({ productFormData: data}),
    categoryFormData : {
        categoryId: 0,
        name: "",
        description: ""
      },
        setCategoryFormData: (data) => set({ categoryFormData: data})
}));

export default useModelStore ;