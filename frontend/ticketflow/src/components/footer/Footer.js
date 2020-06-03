import React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { makeStyles } from "@material-ui/core/styles";
import { Typography } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";

const useStyles = makeStyles(() => ({
    appBar: {
        top: "auto",
        bottom: 0,
        backgroundColor: "white",
        color: "grey" 
    },
}));

function Footer() {
    const styles = useStyles();

    return (
        <AppBar position="static" className={styles.appBar}>
            <Toolbar>
                <Grid container justify="space-between" direction="row">
                    <Grid item>
                        <Typography>TicketFlow - All rights are reserved</Typography>
                    </Grid>
                    <Grid item>
                        <Typography>Contact with us: ticketflow-support@gmail.com</Typography>
                    </Grid>
                </Grid>
            </Toolbar>
        </AppBar>
    );
}

export default Footer;