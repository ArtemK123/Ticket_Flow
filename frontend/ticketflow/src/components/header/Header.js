import React, { useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import HeaderUserPart from "components/header/HeaderUserPart";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import createBackendService from "services/backend_service/createBackendService";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";

Header.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    username: PropTypes.string,
    logoutCallback: PropTypes.func,
    redirectCallback: PropTypes.func,
};

const useStyles = makeStyles(() => ({
    root: {
        flexGrow: 1,
    },
    header: {
        backgroundColor: "Gainsboro",
    },
    homeLink: {
        color: "black",
        textDecoration: "none",
        "&:hover": {
            color: "black",
        }
    }
}));

function Header(props) {
    const styles = useStyles();
    const backendService = createBackendService();

    const [actionState, changeActionState] = useState("");

    useEffect(() => {
        if (actionState === "login") {
            props.redirectCallback("/login");
        }
        else if (actionState === "register") {
            props.redirectCallback("/register");
        }
        else if (actionState === "profile") {
            props.redirectCallback("/profile");
        }
        else if (actionState === "logout") {
            props.logoutCallback();
        }
        changeActionState("");
    }, [actionState, backendService, props]);

    return (
        <div className={styles.root}>
            <AppBar position="static" className={styles.header}>
                <Toolbar>
                    <h3><Link to="/" className={styles.homeLink}>TicketFlow</Link></h3>    
                    <HeaderUserPart
                        isUserLoggedIn={props.isUserLoggedIn}
                        username={props.username}
                        loginAction={() => changeActionState("login")}
                        registerAction={() => changeActionState("register")}
                        profileAction={() => changeActionState("profile")}
                        logoutAction={() => changeActionState("logout")}
                    />
                </Toolbar>
            </AppBar>
        </div>
    );
}

export default Header;