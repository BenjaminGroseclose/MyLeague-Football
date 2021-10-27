import { Typography } from '@mui/material';
import React from 'react'
import MyLeagueAppBar from '../../shared/MyLeagueAppBar';

function SettingsPage() {
  return (
    <div id="SettingsPage">
      <MyLeagueAppBar />
      <Typography variant="h4">
        Settings Page
      </Typography>
    </div>
  )
}

export default SettingsPage;