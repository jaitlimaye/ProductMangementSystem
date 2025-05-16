import { Box, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import type React from 'react';

interface RowActionProps {
    onEdit: () => void;
    onDelete: () => void;
}
const RowAction : React.FC<RowActionProps> = ({onEdit,onDelete}) => {
    return (
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            justifyContent: 'space-between',
            paddingX: 4,
            gap: 2,
          }}>
        <IconButton edge="end" onClick={() => onEdit()} aria-label="edit">
          <EditIcon />
        </IconButton>
        <IconButton edge="end" onClick={() => onDelete()} aria-label="delete">
          <DeleteIcon />
        </IconButton>
      </Box>
    );
}

export default RowAction;