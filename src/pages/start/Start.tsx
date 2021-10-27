import { Button, Link, Typography } from '@mui/material';
import './Start.css';
import React from 'react';

function StartPage() {

  function exit() {
    const browserWindow = require('electron').BrowserWindow
    let window = browserWindow.getCurrentWindow();
    window.quit()
  }

  return (
    <div id="StartPage">
      <Typography variant="h4">
        MyLeague Football
      </Typography>
      <div className="action-buttons">
        <Link href="home" underline="none">   
          <Button variant="contained" sx={{ minWidth: 500, margin: '12px auto' }}>
            Start
          </Button>
        </Link>    
        <Button variant="contained" sx={{ minWidth: 500, margin: '12px auto' }}>
          Load
        </Button>
        <Link href="settings" underline="none">   
          <Button variant="contained" sx={{ minWidth: 500, margin: '12px auto' }}>
            Settings
          </Button>
        </Link>
        <Button variant="contained" sx={{ minWidth: 500, margin: '12px auto' }} onClick={() => exit()}>
          Exit
        </Button>
      </div>

      <div>
        <Typography variant="caption">
          By Ben Groseclose
        </Typography>
      </div>
    </div>
  )
}

export default StartPage;