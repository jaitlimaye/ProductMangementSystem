import { Box, Button, FormControl, Input, InputLabel, MenuItem, Select, Stack, TextField, Typography, type SelectChangeEvent } from "@mui/material";
import { useState } from "react";
import { useSnackbarStore, useUIStore } from "../../utils/stores";
import useDataStore from "../../utils/stores/dataStore";
import { type createProductRequesttype } from "../../utils/types";
import { postProductData } from "../../utils/api/apiservice";

const CreateProductModal = () => {
    const { setModalOpen } = useUIStore();
    const { categorylistdata } = useDataStore();
    const {showSnackbar } = useSnackbarStore();
    const [form, setForm] = useState<createProductRequesttype>({
        name: "",
        description: "",
        price: "",
        categoryId: undefined,
        image: null as File | null,
      });

      const  handleSave = async () => {
        // Handle save logic here
        if (!form.name.trim() || !form.price.trim() || form.categoryId === undefined) {
            showSnackbar("Please fill in all required fields.","error");
            
            return;
          }
          // Handle save logic here
          let res = await postProductData(form);
          if (res) {
            showSnackbar("Product created successfully.","success");
            setForm({
                name: "",
                description: "",
                price: "",
                categoryId: undefined,
                image: null,
              });
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
            setForm((prev) => ({ ...prev, image: e.target.files![0] }));
        }
    };
    return (
        <>
        <Typography id="settings-modal-title" variant="h5" component="h2" gutterBottom sx={{ mb: 2 }}>
          Create Product
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
            <Button variant="outlined" component="label" fullWidth>
              {form.image ? form.image.name : "Upload Image"}
              <input
                type="file"
                accept="image/*"
                hidden
                onChange={handleFileChange}
              />
            </Button>
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
  
  export default CreateProductModal;

/*

*/