import { Typography } from '@mui/material';
import React from 'react';
import MyLeagueAppBar from '../../shared/MyLeagueAppBar';

function HomePage() {
  return (
    <div id="HomePage">
      <MyLeagueAppBar />
      <Typography variant="h4">
        Home Page
      </Typography>
    </div>
  )
}

export default HomePage;