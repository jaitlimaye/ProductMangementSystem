import React, {useEffect} from 'react';
import Navbar from '../components/Navbar';
import Sidebar from '../components/Sidebar'
import { Box , Typography } from '@mui/material';
import { useUIStore, useDataStore,useAuthStore } from '../utils/stores';
import { ItemList } from '../components/ItemList';
import { ProductRow,UserRow,CategoryRow } from '../components/Rows';
import { getAllCategories, getAllProducts } from '../utils/api/apiservice';
import ModalDialog from '../components/Modal';



const Home: React.FC = () => {
  const {roles} = useAuthStore();
  const { listdisplay,setLoading, isModalOpen,setModalOpen, setModalType } = useUIStore();
  const { setProductListData, setCategoryListData, setUserListData, productlistdata, categorylistdata,userlistdata } = useDataStore();
  useEffect(() => {
    async function fetchData() {
      switch (listdisplay) {
        case 'Products':
          setLoading(true);
          const proddata = await getAllProducts();
          setProductListData(proddata);
          if (categorylistdata.length === 0) {
            const catdata = await getAllCategories();
            setCategoryListData(catdata);
          }
          setLoading(false);
          break;
        case 'Categories':
          setLoading(true);
          const catdata = await getAllCategories();
          setCategoryListData(catdata);
          setLoading(false);
          break;
        case 'Users':
          setUserListData([
            {  email: 'alice@example.com' },
            {  email: 'bob@example.com' },
          ]);
          break;
        default:
          break;
      }
    }
    
    fetchData();
   
  }, [listdisplay]);
  
  const handleCreateClick = (type : string) => {
    setModalType(type);
    setModalOpen(true);
  }
  const RenderList: React.FC  = () => {
    switch (listdisplay) {
      case 'Products':
        return(
        <ItemList items={productlistdata} RowComponent={ProductRow} title='Products' onCreateClick={() => handleCreateClick("CreateUser")}/>
        );
      case 'Categories':
        return(
        <ItemList items={categorylistdata} RowComponent={CategoryRow} title='Categories' onCreateClick={() => handleCreateClick("CreateCategory")}/>

        );
      default:
        return null;
    }
  };
  return (
    <>
    <Box sx={{ display: 'flex', flexDirection: 'column', height: '100vh', minWidth: '100%' }}>
      <Navbar />
      <Box sx={{ display: 'flex', flex: 1,  minWidth: '100%' }}>
      {roles.includes('Admin') && 
        <Sidebar /> }
        <Box sx={{ display: 'flex', flexDirection: 'column',width: '100%' , gap: 4, p: 8 }}>
          <RenderList />
        </Box>
      </Box>
    </Box>
    {isModalOpen && (
        <ModalDialog onSave={() => setModalOpen(false)} />
      )}
    </>
  );
};

export default Home;
