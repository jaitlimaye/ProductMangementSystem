import { Button, Stack, TextField, Typography} from "@mui/material";
import { useState } from "react";
import { useSnackbarStore, useUIStore } from "../../utils/stores";
import { type createCategoryRequesttype } from "../../utils/types";
import { postCategoryData } from "../../utils/api/apiservice";

const CreateCategoryModal = () => {
    const { setModalOpen } = useUIStore();
    const {showSnackbar } = useSnackbarStore();
    const [form, setForm] = useState<createCategoryRequesttype>({
        name: "",
        description: ""
      });

      const  handleSave = async () => {
        // Handle save logic here
        if (!form.name.trim() ) {
            showSnackbar("Please fill in all required fields.","error");
            
            return;
          }
          // Handle save logic here
          let res = await postCategoryData(form);
          if (res) {
            showSnackbar("Category created successfully.","success");
            setForm({
                name: "",
                description: ""
              });
            }
            else {
                showSnackbar("Error creating category.","error");
            }
          setModalOpen(false);
      }
      const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

     
    return (
        <>
        <Typography id="settings-modal-title" variant="h5" component="h2" gutterBottom sx={{ mb: 2 }}>
          Create Category
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
  
  export default CreateCategoryModal;
