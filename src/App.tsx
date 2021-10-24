import './App.css';
import { ThemeProvider } from '@emotion/react';
import { theme } from './theme';
import { useEffect } from 'react';
import { BrowserRouter, Route } from "react-router-dom"
import { InitJsStore } from './database/DatabaseService';
import StartPage from './pages/start/Start';
import HomePage from './pages/home/Home';

function App() {
  useEffect(() => {
    async function setupDatabase() {
      await InitJsStore();
    }

    setupDatabase();
  }, []);

  function Content() {
    return (
      <div id="App">
        <Route exact path="/home" component={HomePage} />
        <Route exact path="/" component={StartPage} />
      </div>
    );
  }

  return (
    <ThemeProvider theme={theme}>
      <BrowserRouter>
        <Content />
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
