import { Button } from '@mui/material';

type buttonProps = {
    text: string;
}
export const SubmitButton: React.FC<buttonProps> = ({ text }) => {
    return (
      <Button
        type="submit"
        fullWidth
        variant="contained"
        sx={{ mt: 3, mb: 2 }}
      >
        {text}
      </Button>
    );
  };
