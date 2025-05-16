import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import useAuthStore from '../utils/stores/authStore';
import useSnackbarStore from '../utils/stores/snackbarStore';
const Navbar: React.FC = () => {
    const {logout} = useAuthStore();
    const {showSnackbar } = useSnackbarStore();
      
    const handleLogout = () => {
        showSnackbar(`Logged out Successfully!`,"success");
        logout();
        window.location.href = '/login';
    };

    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" style={{ flexGrow: 1 }}>
                    Product Mangement
                </Typography>
                <Button color="inherit" onClick={handleLogout} sx = {{border: 1, borderColor: "white"}}>
                    Logout
                </Button>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;