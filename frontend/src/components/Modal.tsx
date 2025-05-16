import React from 'react';
import { Modal, Box } from '@mui/material';
import { useUIStore } from '../utils/stores';
import { CreateProductModal,DeleteProductModal,EditProductModal,CreateCategoryModal, EditCategoryModal, DeleteCategoryModal } from './ModalTypes';
interface ModalDialogProps {
  onSave: () => void;
}

interface ModalRendersType {
  [key: string]: React.ReactNode;
}

const modalRenders: ModalRendersType = {
  "CreateUser": <CreateProductModal />,
  "EditProduct" : <EditProductModal />,
  "DeleteProduct": <DeleteProductModal />,
  "CreateCategory": <CreateCategoryModal />,
  "EditCategory": <EditCategoryModal />,
  "DeleteCategory": <DeleteCategoryModal />,
  // Add other modal types here
};

const ModalDialog: React.FC<ModalDialogProps> = ({onSave }) => {
  const { setModalOpen, modalType} = useUIStore();
  
  return (
    <Modal open onClose={() => setModalOpen(false)} aria-labelledby="settings-modal-title">
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 400,
          bgcolor: 'background.paper',
          boxShadow: 24,
          p: 4,
          borderRadius: 2,
        }}
      >
        {modalRenders[modalType]}
      </Box>
    </Modal>
  );
};

export default ModalDialog;
