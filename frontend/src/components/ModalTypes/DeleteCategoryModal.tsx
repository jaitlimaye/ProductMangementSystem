import { Button, Stack, Typography } from "@mui/material";
import { useModelStore, useSnackbarStore, useUIStore } from "../../utils/stores";
import { deleteCategoryData } from "../../utils/api/apiservice";

const DeleteCategoryModal = () => {
    const { setModalOpen } = useUIStore();
    const { categoryFormData} = useModelStore();
    const {showSnackbar } = useSnackbarStore();

      const  handleConfirm = async () => {
            await deleteCategoryData(categoryFormData.categoryId);
            showSnackbar("Product deleted successfully.","success");
          setModalOpen(false);
      }
    return (
        <>
        <Typography id="settings-modal-title" variant="h5" component="h2" gutterBottom sx={{ mb: 2 }}>
          Delete Product
        </Typography>
        
          Are you sure you want to delete {categoryFormData.name}? This action cannot be undone.
        <Stack direction="row" spacing={2} justifyContent="flex-end" sx={{ mt: 4 }}>
          <Button variant="outlined" color="secondary" onClick={() => setModalOpen(false)}>
            Cancel
          </Button>
          <Button variant="contained" color="primary" onClick={() => handleConfirm()}>
            Delete
          </Button>
        </Stack>
      </>
    );
  };
  
  export default DeleteCategoryModal;