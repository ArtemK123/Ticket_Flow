import React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { makeStyles } from "@material-ui/core/styles";
import { Typography } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";

const useStyles = makeStyles((theme) => ({
    text: {
        padding: theme.spacing(2, 2, 0),
    },
    paper: {
        paddingBottom: 50,
    },
    list: {
        marginBottom: theme.spacing(2),
    },
    subheader: {
        backgroundColor: theme.palette.background.paper,
    },
    appBar: {
        top: "auto",
        bottom: 0,
        backgroundColor: "white",
        color: "grey" 
    },
    grow: {
        flexGrow: 1,
    },
    fabButton: {
        position: "absolute",
        zIndex: 1,
        top: -30,
        left: 0,
        right: 0,
        margin: "0 auto",
    },
}));

function Footer() {
    const styles = useStyles();

    return (
        <AppBar position="fixed" className={styles.appBar}>
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