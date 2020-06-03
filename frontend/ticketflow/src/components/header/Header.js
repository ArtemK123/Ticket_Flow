import React, { useState, useEffect } from "react";
import Grid from "@material-ui/core/Grid";
import { makeStyles } from "@material-ui/core/styles";
import HeaderUserPart from "components/header/HeaderUserPart";
import { useHistory } from "react-router-dom";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import createBackendService from "services/backend_service/createBackendService";

Header.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    username: PropTypes.string,
    reloadParent: PropTypes.func,
};

const useStyles = makeStyles(() => ({
    root: {
        flexGrow: 1,
        backgroundColor: "Gainsboro",
    },
    second: {
        backgroundColor: "red",
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
    const history = useHistory();
    const backendService = createBackendService();

    const [actionState, changeActionState] = useState("");

    useEffect(() => {
        if (actionState === "login") {
            history.push("/login");
        }
        else if (actionState === "register") {
            history.push("/register");
        }
        else if (actionState === "profile") {
            history.push("/profile");
        }
        else if (actionState === "logout") {
            const token = localStorage.getItem("token");
            backendService.logout(token).then(() => {
                localStorage.removeItem("token");
                localStorage.removeItem("username");
            });
            props.reloadParent();
        }
        changeActionState("");
    }, [actionState, history, backendService, props]);

    return (
        <Grid container justify="space-around" alignItems="center" className={styles.root} spacing={2}>
            <Grid item xs={9}>
                <h3><Link to="/" className={styles.homeLink}>TicketFlow</Link></h3>    
            </Grid>
            <Grid item xs={3}>
                <HeaderUserPart
                    isUserLoggedIn={props.isUserLoggedIn}
                    username={props.username}
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