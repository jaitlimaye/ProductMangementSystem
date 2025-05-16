import React from 'react';
import { List , Box, Typography, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import { useDataStore, useUIStore, useAuthStore } from '../utils/stores';
interface ListProps<T> {
    title: string;
  items: T[];
  RowComponent: React.ComponentType<{ item: T }>;
  onCreateClick?: () => void;
}

export function ItemList<T>({ items, RowComponent,title, onCreateClick}: ListProps<T>) {
  const {loading} = useUIStore();
  const {roles} = useAuthStore();
  const { categorylistdata, productFilter, setProductFilter } = useDataStore();
  if (loading) {
    return (
      <Box sx={{width : '100%', padding: 2}}>
        <Typography variant="h5">{title}</Typography>
        <h3>Loading...</h3>
      </Box>
    );
  } 
  const handleFilter = (event : any) => {
    const value = event.target.value;
    setProductFilter(value === "" ? null : parseInt(value));
  }
  return (
    <Box sx={{width : '100%', padding: 2}}>
      <Box sx={{ display: 'flex',flexDirection : 'row', justifyContent: 'space-between', alignItems: 'center' }}>
      <Typography variant="h5">{title}</Typography>
      {title === "Products" && 
        <FormControl required  sx={{ mt: 3, mb: 2, width: '30%' }}>
          <InputLabel id="category-label">Category</InputLabel>
            <Select
                    labelId="category-label"
                    label="Category"
                    name="categoryId"
                    value={productFilter?.toString() || null}  
                    onChange={(event)=> {handleFilter(event)}}
                  >
                    <MenuItem key="all" value="">
                      All
                    </MenuItem>
                    {categorylistdata.map((cat) => (
                      <MenuItem key={cat.categoryId} value={cat.categoryId}>
                        {cat.name}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>}

        {onCreateClick && roles.includes('Admin') &&  (
          <Button
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2, width: '20%' }}
          onClick={onCreateClick}
        >
          Create New
        </Button>
        )}
        </Box>
        
        <List>
            {items.map((item) => {
        if (title === "Products" && productFilter !== null && (item as any).categoryId !== productFilter) {
          return null;}
                return <RowComponent key={(item as any).id} item={item} />
      })}
        </List>
    </Box>
  );
}

