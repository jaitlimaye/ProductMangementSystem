import React from 'react';
import { Snackbar, Alert } from '@mui/material';
import useSnackbarStore from '../utils/stores/snackbarStore';

const SnackbarHandler: React.FC = () => {
    const { isOpen, message, severity, closeSnackbar } = useSnackbarStore();

    const handleClose = () => {
        closeSnackbar();
    };

    return (
        <Snackbar
            open={isOpen}
            autoHideDuration={6000}
            onClose={handleClose}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
        >
            <Alert onClose={handleClose} severity={severity} sx={{ width: '100%' }}>
                {message}
            </Alert>
        </Snackbar>
    );
};

export default SnackbarHandler;