// Sidebar.tsx
import React, { useState } from 'react';
import {
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  IconButton,
  Tooltip,
} from '@mui/material';
import { Box } from '@mui/system';
import {
  Menu as MenuIcon,
  Category as CategoryIcon,
  Inventory as ProductIcon,
  People as UsersIcon,
  ChevronLeft,
  ChevronRight,
} from '@mui/icons-material';
import useUIStore
 from '../utils/stores/uiStore';
const drawerWidth = 200;

const Sidebar = () => {
  const [open, setOpen] = useState(true);
  const { setListDisplay } = useUIStore();
  const toggleDrawer = () => setOpen(!open);

  const menuItems = [
    { text: 'Products', icon: <ProductIcon /> },
    { text: 'Categories', icon: <CategoryIcon /> }
  ];

  return (
    <Box sx={{ display: 'flex' }}>
      <Drawer
  variant="permanent"
  open={open}
  sx={{
    width: open ? drawerWidth : 60,
    flexShrink: 0,
    '& .MuiDrawer-paper': {
      width: open ? drawerWidth : 60,
      marginTop: '64px', // height of AppBar
      transition: 'width 0.3s',
      overflowX: 'hidden',
    },
  }}
>
        <Box sx={{ display: 'flex', justifyContent: open ? 'flex-end' : 'center', p: 1 }}>
          <IconButton onClick={toggleDrawer}>
            {open ? <ChevronLeft /> : <ChevronRight />}
          </IconButton>
        </Box>
        <List>
          {menuItems.map((item) => (
            <Tooltip title={!open ? item.text : ''} placement="right" key={item.text}>
                <ListItem >
                    <ListItemButton onClick={() => setListDisplay(item.text)}>
                    <ListItemIcon>{item.icon}</ListItemIcon>
                    <ListItemText primary={open ? item.text : ''} />
                    </ListItemButton>
                </ListItem>
            </Tooltip>
          ))}
        </List>
      </Drawer>
    </Box>
  );
};

export default Sidebar;
