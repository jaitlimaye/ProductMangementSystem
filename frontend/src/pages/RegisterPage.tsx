import { useMutation } from '@tanstack/react-query';
import { useForm } from 'react-hook-form';
import { TextField, Box, Typography, Container, Select, InputLabel, MenuItem, FormControl } from '@mui/material';
import { SubmitButton } from '../components/Buttons';
import { Link, useNavigate } from "react-router";
import { getAllRoles, postRegisterData } from '../utils/api/apiservice';
import type { registerRequest } from '../utils/types';
import useSnackbarStore from '../utils/stores/snackbarStore';
import { useState,useEffect } from 'react';
import type { Role } from '../utils/types/data/datatype';

const RegisterPage = () => {
  const [roles, setRoles] = useState<Role[]>([]);

  useEffect(() => {
    const fetchRoles = async () => {
      try {
        
        const data = await getAllRoles();
        setRoles(data);
      } catch (error) {
        console.error('Error fetching roles:', error);
      }
    };

    fetchRoles();
  }, []);
  const { register, handleSubmit, formState: { errors } } = useForm<registerRequest>();
  const {showSnackbar } = useSnackbarStore();
  const navigate = useNavigate();
  const mutation = useMutation({
    mutationFn: (req :registerRequest) => postRegisterData(req),
    onSuccess: () => {
      showSnackbar(`Registration Successful`,"success");
      navigate('/login'); // Redirect to login page after successful registration
    },
    onError: (error) => {
      showSnackbar(`Login failed:${error.message}`,"error");
    },
  });

  const onSubmit = (data: registerRequest) => {
    console.log(data);
    if (data.password !== data.confirmPassword) {
      showSnackbar('Passwords do not match!',"error");
      return;
    }
    mutation.mutate(data); // Submit only email and password
  };

  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          marginTop: 8,
        }}
      >
        <Typography variant="h4" component="h1" gutterBottom>
          Register
        </Typography>
        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            {...register('email', { required: 'Email is required' })}
            error={!!errors.email}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            {...register('username', { required: 'Username is required' })}
            error={!!errors.username}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="password"
            label="Password"
            type="password"
            {...register('password', { required: 'Password is required' })}
            error={!!errors.password}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="confirmPassword"
            label="Confirm Password"
            type="password"
           {...register('confirmPassword', { required: 'Confirm Password is required' })}
            error={!!errors.confirmPassword}
          />
          <FormControl fullWidth>
          <InputLabel id="role-label">Role</InputLabel>
          <Select
            fullWidth
            labelId="role-label"
            label="Role"
            {...register('roleId', { required: 'Role is required' })}
            error={!!errors.roleId}
            defaultValue="">
          
            {roles.map((role) => (
              <MenuItem key={role.roleId} value={role.roleId}>
                {role.roleName}
              </MenuItem>
            ))}
              </Select>
              </FormControl>
          <SubmitButton text="Register"  />
          <Typography variant="body2" sx={{ mt: 2 }}>
            Already have an account? <Link to="/login">Login</Link>
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default RegisterPage;
