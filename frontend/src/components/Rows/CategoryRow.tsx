import {type Category } from '../../utils/types';
import { ListItem, ListItemText } from '@mui/material';
import RowAction from './RowAction';
import { useUIStore, useModelStore } from '../../utils/stores';
const CategoryRow: React.FC<{ item: Category }> = ({ item }) =>  {
    const { setModalOpen, setModalType } = useUIStore();
    const { setCategoryFormData } = useModelStore();
    const handleEdit = async () => {
      setCategoryFormData(item);
      setModalType("EditCategory");
      setModalOpen(true);
    }
    const handleDelete = async () => {
      setCategoryFormData(item);
      setModalType("DeleteCategory");
      setModalOpen(true);
    }
    
    return (   

  
    <ListItem
    sx={{
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        p: 2,
        paddingX : 5,
        border: '1px solid #ccc',
      }}
    secondaryAction={
      <RowAction onDelete={() => {handleDelete()}} onEdit={()=>{handleEdit()}} />
    }
  >
    <ListItemText primary={item.name} secondary={item.description} />
   
  </ListItem>
)}

export default CategoryRow;