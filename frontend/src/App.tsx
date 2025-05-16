import './App.css'
import { RouterService } from './utils/routes/RouterService'
import SnackbarHandler from './components/SnackbarHandler'
import { QueryClientProvider,QueryClient } from '@tanstack/react-query'

const queryClient = new QueryClient()
function App() {

  return (
    <QueryClientProvider client={queryClient}>
      <RouterService />
      <SnackbarHandler />
    </QueryClientProvider>
    )
}

export default App
