import { Link, Typography } from "@mui/material";
import React from "react";

const Copyright = (props: any) => {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
                FitApp
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}
export default Copyright