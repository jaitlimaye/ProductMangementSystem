import axios from 'axios';
import type {Category, registerRequest, loginRequest, Product, createProductRequesttype, createCategoryRequesttype} from '../types/index';
import { useAuthStore } from '../stores';

const API_BASE_URL = 'https://localhost:7291/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

const authConfig = () => {
  const token = useAuthStore.getState().token;
  return token
    ? { headers: { 'Authorization': `Bearer ${token}` } }
    : {};
}
const multiPartAuthConfig = () => {
  const token = useAuthStore.getState().token;
  return token
    ? { headers: { 'Authorization': `Bearer ${token}`,'Content-Type': 'multipart/form-data', } }
    : {'Content-Type': 'multipart/form-data'};
}
//Auth API
export const postLoginData = async (data : loginRequest) => {
  const response = await api.post(`/Auth/login`, data);
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
}

export const postRegisterData = async (data : registerRequest) => {
  console.log('Data to Register:', data); // Log the data being sent
  
  const response = await api.post(`/Auth/register`, data);
  console.log('Response to Register:', response); // Log the entire response object
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
  
 return true;
}

//Category API
export const getAllCategories = async (): Promise<Category[]>  => {
  const response = await api.get(`/Category`);
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  console.log('Response to Categories:', response); // Log the entire response object
  return response.data; // Return only the data property
}

export const postCategoryData = async (data : createCategoryRequesttype) => {
  const response = await api.post(`/Category`, data, authConfig());
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
}

export const editCategoryData = async (id : string, data : Category) => {
  const response = await api.put(`/Category/${id}`, data, authConfig());
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
}
export const deleteCategoryData = async (id : number) => {
  await api.delete(`/Category/${id}`, authConfig());
  return true;
}
//Product API 

export const getAllProducts = async (): Promise<Product[]>  => {
  const response = await api.get(`/Product`, authConfig())
  if(!response.data) {  
    throw new Error('No data found in the response');
  }
  console.log('Response to Products:', response); // Log the entire response object
  return response.data; // Return only the data property
}

export const postProductData = async (data : createProductRequesttype) => {
  const response = await api.post(`/Product`, data, multiPartAuthConfig());
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
}

export const editProductData = async (id : string, data : createProductRequesttype) => {
  const response = await api.put(`/Product/${id}`, data, multiPartAuthConfig());
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  return response.data; // Return only the data property
}

export const deleteProductData = async (id : number) => {
  await api.delete(`/Product/${id}`, authConfig());
  return true;
}

//Role API
export const getAllRoles = async () => {
  const response = await api.get(`/Role`);
  if(!response.data) {
    throw new Error('No data found in the response');
  }
  console.log('Response to Roles:', response); // Log the entire response object
  return response.data; // Return only the data property
}