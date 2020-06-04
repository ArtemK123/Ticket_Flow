import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import createBackendService from "services/backend_service/createBackendService";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Button from "@material-ui/core/Button";
import EmailInput from "components/common/EmailInput";
import PasswordInput from "components/common/PasswordInput";
import PropTypes from "prop-types";
import { Typography, Grid, Box } from "@material-ui/core";

LoginPage.propTypes = {
    reloadParent: PropTypes.func,
};

const useStyles = makeStyles((theme) => ({
    root: {
        "& > *": {
            margin: theme.spacing(1),
            width: "25ch",
        },
    }
}));

function LoginPage(props) {
    const styles = useStyles();
    const backendService = createBackendService();
    const history = useHistory();
    const [inputState, changeInputState] = useState({
        email: "",
        password: "",
        passwordHelperText: "",
        emailHelperText: ""
    });

    const [loginState, changeLoginState] = useState({
        submitCalled: false,
        userNotFound: false,
        invalidPassword: false,
    });

    useEffect(() => {
        if (loginState.submitCalled) {
            loginState.submitCalled = false;
            sendLoginRequest();
        }
    });

    const sendLoginRequest = () => {
        backendService.login({
            email: inputState.email,
            password: inputState.password
        }).then(async response => {
            if (response.ok) {
                const jwtToken = await response.text();
                localStorage.setItem("token", jwtToken);
                localStorage.setItem("username", inputState.email);
                history.push("/");
                props.reloadParent();
                return;
            }
            else if (response.status === 401) {
                loginState.invalidPassword = true;
            }
            else if (response.status === 404) {
                loginState.userNotFound = true;
            }
            changeLoginState(Object.assign({}, loginState));
        });
    };

    const handleFormSubmit = (event) => {
        event.preventDefault();
        changeLoginState(Object.assign({}, loginState, {submitCalled: true}));
    };

    const handleInputChange = (stateTarget, event) => {
        changeInputState(Object.assign({}, inputState, {[stateTarget]: event.target.value}));
    };

    return (
        <Box w={1}>
            <Grid container direction="row">
                <Grid item xs={1}/>
                <Grid item xs={9}>
                    <Box m={5}>
                        <form className={styles.root} onSubmit={handleFormSubmit}>
                            <Grid container direction="column" spacing={2}>
                                <Grid item><Typography variant="h4">Login</Typography></Grid>
                                <Grid item>
                                    <EmailInput
                                        label="Email"
                                        value={inputState.email}
                                        onChange={event => handleInputChange("email", event)}
                                        isErrorState={loginState.userNotFound}
                                        helperText="User with given email doesn`t exist"
                                    />
                                </Grid>
                                <Grid item>
                                    <PasswordInput
                                        label="Password"
                                        value={inputState.password}
                                        onChange={event => handleInputChange("password", event)}
                                        isErrorState={loginState.invalidPassword}
                                        helperText="Wrong password"
                                    />
                                </Grid>
                                <Grid item>
                                    <Button variant="contained" color="primary" type="submit">Submit</Button>
                                </Grid>
                            </Grid>
                        </form>
                    </Box>
                </Grid>
                <Grid item xs={2}/>
            </Grid>
        </Box>
    );
}

export default LoginPage;