import { Button, Link, Typography } from '@mui/material';
import React from 'react';

function StartPage() {
  return (
    <>
      <Typography variant="h2">
        Start Page
      </Typography>
      <Link href="home" underline="none">   
        <Button variant="contained">
          Home
        </Button>
      </Link>
    </>
  )
}

export default StartPage;