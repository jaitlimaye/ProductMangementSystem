import { Box, Button, FormControl, IconButton,  InputLabel, MenuItem, Select, Stack, TextField, Typography, type SelectChangeEvent } from "@mui/material";
import { useState } from "react";
import { useSnackbarStore, useUIStore,useModelStore } from "../../utils/stores";
import useDataStore from "../../utils/stores/dataStore";
import { type editProductRequesttype  } from "../../utils/types";
import DeleteIcon from '@mui/icons-material/Delete';
import { editProductData } from "../../utils/api/apiservice";

const EditProductModal = () => {
    const { setModalOpen } = useUIStore();
    const {productFormData} = useModelStore();
    const { categorylistdata } = useDataStore();
    const {showSnackbar } = useSnackbarStore();
    let currentImageUrl = productFormData.imageUrl == "/uploads/default.jpg" ? null : productFormData.imageUrl;
    const [form, setForm] = useState<editProductRequesttype >({
        productId: productFormData.productId,
        name: productFormData.name,
        description: productFormData.description? productFormData.description : "",
        price: productFormData.price.toString(),
        categoryId: productFormData.categoryId,
        image: null as File | null,
        currentImageUrl: currentImageUrl? currentImageUrl : null,
      });

      const  handleSave = async () => {
        // Handle save logic here
        if (!form.name.trim() || !form.price.trim() || form.categoryId === undefined) {
            showSnackbar("Please fill in all required fields.","error");
            
            return;
          }
          console.log(productFormData.productId,form);
          let res = await editProductData(productFormData.productId.toString(),form);
          
          if (res) {
            showSnackbar("Product created successfully.","success");
          
            }
            else {
                showSnackbar("Error creating product.","error");
            }
          setModalOpen(false);
      }
      const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

      const handleSelectChange = (e: SelectChangeEvent) => {
        
        setForm((prev) => ({ ...prev, "categoryId": Number(e.target.value) }));
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            setForm((prev) => ({ ...prev, image: e.target.files![0],currentImageUrl: null }));
        }
    };

    const handleDeleteImage = () => {
      setForm((prev) => ({
        ...prev,
        image: null, // Mark for deletion
        currentImageUrl: null, // Remove preview
      }));
    };
    return (
        <>
        <Typography id="settings-modal-title" variant="h5" component="h2" gutterBottom sx={{ mb: 2 }}>
          Edit Product
        </Typography>
        <Stack spacing={3} sx={{ minWidth: 350 }}>
          <TextField
            label="Name"
            name="name"
            value={form.name}
            onChange={handleInputChange}
            required
            fullWidth
          />
          <TextField
            label="Description"
            name="description"
            value={form.description}
            onChange={handleInputChange}
            multiline
            rows={3}
            fullWidth
          />
          <TextField
            label="Price"
            name="price"
            type="number"
            value={form.price}
            onChange={handleInputChange}
            required
            fullWidth
            inputProps={{ min: 0, step: "0.01" }}
          />
          <FormControl fullWidth required>
            <InputLabel id="category-label">Category</InputLabel>
            <Select
              labelId="category-label"
              label="Category"
              name="categoryId"
              value={form.categoryId?.toString() || ""}
              onChange={handleSelectChange}
            >
              {categorylistdata.map((cat) => (
                <MenuItem key={cat.categoryId} value={cat.categoryId}>
                  {cat.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <Box>
          {form.currentImageUrl? (
            <Box sx={{ display: "flex", alignItems: "center", mb: 1 }}>
              <img
                src={`https://localhost:7291${form.currentImageUrl}`}
                alt="Current"
                style={{ width: 80, height: 80, objectFit: "cover", borderRadius: 4, marginRight: 8 }}
              />
              <IconButton aria-label="delete" onClick={handleDeleteImage}>
                <DeleteIcon />
              </IconButton>
            </Box>
          ): (<Button variant="outlined" component="label" fullWidth>
            {form.image && typeof form.image !== "string"
              ? form.image.name
              : "Upload Image"}
            <input
              type="file"
              accept="image/*"
              hidden
              onChange={handleFileChange}
            />
          </Button>) }
          
        </Box>
        </Stack>
        <Stack direction="row" spacing={2} justifyContent="flex-end" sx={{ mt: 4 }}>
          <Button variant="outlined" color="secondary" onClick={() => setModalOpen(false)}>
            Cancel
          </Button>
          <Button variant="contained" color="primary" onClick={() => handleSave()}>
            Save
          </Button>
        </Stack>
      </>
    );
  };
  
  export default EditProductModal;

/*

*/