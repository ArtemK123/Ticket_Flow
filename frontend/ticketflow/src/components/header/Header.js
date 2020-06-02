import React, { useState, useEffect } from "react";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import HeaderUserPart from "components/header/HeaderUserPart";

const useStyles = makeStyles(() => ({
    root: {
        flexGrow: 1,
        backgroundColor: "Gainsboro",
    },
    second: {
        backgroundColor: "red",
    }
}));

function Header() {
    const styles = useStyles();
    const [headerState, changeHeaderState] = useState({
        isUserLoggedIn: false,
        username: "",
        initialized: false
    });

    useEffect(() => {
        if (!headerState.initialized) {
            headerState.initialized = true;
            const token = localStorage.getItem("token");
            headerState.isUserLoggedIn = token !== null;
            const usernameInStorage = localStorage.getItem("username");
            headerState.username = usernameInStorage ? usernameInStorage : "";
            changeHeaderState(Object.assign({}, headerState));
        }
    }, [headerState]);

    return (
        <Grid container justify="space-around" alignItems="center" className={styles.root} spacing={2}>
            <Grid item xs={9}>
                <h3>TicketFlow</h3>    
            </Grid>
            <Grid item xs={3}>
                <HeaderUserPart
                    isUserLoggedIn={headerState.isUserLoggedIn}
                    username={headerState.username}
                    style={styles.second}
                />
            </Grid>   
        </Grid>
    );
}

export default Header;