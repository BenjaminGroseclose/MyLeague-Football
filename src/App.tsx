import logo from './logo.svg';
import './App.css';
import MyLeagueAppBar from './shared/MyLeagueAppBar';
import { ThemeProvider } from '@emotion/react';
import { theme } from './theme';
import { useEffect } from 'react';
import { InitJsStore } from './database/DatabaseService';

function App() {
  useEffect(() => {
    console.log("App.tsx useEffect")
    InitJsStore();
  }, []);

  function Content() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    );
  }

  return (
    <ThemeProvider theme={theme}>
      <MyLeagueAppBar />
      <Content />
    </ThemeProvider>
  );
}

export default App;
