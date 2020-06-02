import React, { useState, useEffect } from "react";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import HeaderUserPart from "components/header/HeaderUserPart";
import { useHistory } from "react-router-dom";

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
    const history = useHistory();
    const [headerState, changeHeaderState] = useState({
        isUserLoggedIn: false,
        username: "",
        initialized: false
    });

    const [actionState, changeActionState] = useState("");

    useEffect(() => {
        if (!headerState.initialized) {
            headerState.initialized = true;
            const token = localStorage.getItem("token");
            headerState.isUserLoggedIn = token !== null;
            const usernameInStorage = localStorage.getItem("username");
            headerState.username = usernameInStorage ? usernameInStorage : "";
            changeHeaderState(Object.assign({}, headerState));
        }
    });


    useEffect(() => {
        if (actionState === "login") {
            history.push("/login");
            return;
        }
        else if (actionState === "register") {
            history.push("/register");
            return;
        }
        else if (actionState === "profile") {
            history.push("/profile");
            return;
        }
        else if (actionState === "logout") {
            handleLogout();
        }
    });

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("username");
        changeActionState("");
        changeHeaderState(Object.assign({}, {initialized: false}));
    };

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
                    loginAction={() => changeActionState("login")}
                    registerAction={() => changeActionState("register")}
                    profileAction={() => changeActionState("profile")}
                    logoutAction={() => changeActionState("logout")}
                />
            </Grid>   
        </Grid>
    );
}

export default Header;