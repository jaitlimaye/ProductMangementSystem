import { useForm } from 'react-hook-form';
import { TextField, Box, Typography, Container } from '@mui/material';
import { SubmitButton } from '../components/Buttons';
import { Link } from "react-router";
import { useMutation } from '@tanstack/react-query';
import { postLoginData } from '../utils/api/apiservice';
import useAuthStore from '../utils/stores/authStore';
import type  { IAuth } from '../utils/stores/authStore';
import useSnackbarStore from '../utils/stores/snackbarStore';
import type { loginRequest } from '../utils/types/request/loginRequesttype';
const LoginPage = () => {
  const { register, handleSubmit, formState: { errors } } = useForm<loginRequest>();
  const setToken  = useAuthStore((state : IAuth) => state.setToken);
  const {showSnackbar } = useSnackbarStore();
  const mutation = useMutation({
    mutationFn: (data : loginRequest) =>
      postLoginData(data),
    onSuccess: (response) => {
        showSnackbar("Logged in successfully!","success");
        console.log(response.token);
        setToken(response.token);
      },
      onError: (error) => {
        showSnackbar(`Login failed:${error.message}`,"error");
      },}
    
  );

  const onSubmit = (data : loginRequest) => {
    mutation.mutate(data);
  };

  return (
    <Container maxWidth="xs" sx={{
      display: 'flex',
      flexDirection: 'row',
      alignItems: 'center',
      marginTop: 8,
    }}>
      <Box
        sx={{
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
          marginTop: 8,
        }}
      >
        <Typography variant="h4" component="h1" gutterBottom>
          Login
        </Typography>
        <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            {...register('username', { required: 'username is required' })}
            error={!!errors.username}
            autoComplete="username"
            autoFocus
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
            autoComplete="current-password"
          />
          <SubmitButton text="Login" />
          <Typography variant="body2" sx={{ mt: 2 }}>
            Don't have an account? <Link to="/register">Register</Link>
          </Typography>
        </Box>
      </Box>
    </Container>
  );
};

export default LoginPage;
