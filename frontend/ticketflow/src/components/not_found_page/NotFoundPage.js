import React from "react";
import { Link } from "react-router-dom";
import { Box, Typography } from "@material-ui/core";

function NotFoundPage() {
    return (
        <Box m={5}>
            <Typography variant="h4">NotFoundPage</Typography>
            <ul>
                <li><Link to="/">HomePage</Link></li>
            </ul>
        </Box>
    );
}

export default NotFoundPage;