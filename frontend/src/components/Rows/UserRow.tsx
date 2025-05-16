import { type User } from '../../utils/types';
import { ListItem, ListItemText } from '@mui/material';

import RowAction from './RowAction';

const UserRow: React.FC<{ item: User }> = ({ item }) => (
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
      <RowAction onDelete={() => {}} onEdit={()=>{}} />
    }
  >
      <ListItemText primary={item.email} />
    </ListItem>
  );

export default UserRow;