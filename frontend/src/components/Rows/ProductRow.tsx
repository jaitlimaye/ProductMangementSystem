import { type Category, type Product} from '../../utils/types';
import { ListItem, ListItemText,ListItemAvatar,Avatar } from '@mui/material';
import {useDataStore, useUIStore,useModelStore,useAuthStore } from '../../utils/stores';
import RowAction from './RowAction';
const ProductRow: React.FC<{ item: Product }> = ({ item }) => {
  const {roles} = useAuthStore();
  const { setModalOpen, setModalType } = useUIStore();
  const {setProductFormData} = useModelStore();
  const { categorylistdata } = useDataStore();
  const imgurl = `https://localhost:7291${item.imageUrl}`;
  const category = categorylistdata.find((cat : Category) => cat.categoryId === item.categoryId)?.name || 'Unknown' ;

  const handleEdit = async () => {
    setProductFormData(item);
    setModalType("EditProduct");
    setModalOpen(true);
  }
  const handleDelete = async () => {
    setProductFormData(item);
    setModalType("DeleteProduct");
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
      roles.includes('Admin') && <RowAction onDelete={() => {handleDelete()}} onEdit={()=>{handleEdit()}} />
    }
  >
    <ListItemAvatar>
      <Avatar
        variant="square"
        src={imgurl}
        sx={{ width: 64, height: 64, mr: 4 }}
      />
    </ListItemAvatar>
    <ListItemText
      primary={item.name}
      secondary={`₹${item.price.toFixed(2)} • Category: ${category}`}
    />
  </ListItem>
);
}

export default ProductRow;

