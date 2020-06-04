import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import createBackendService from "services/backend_service/createBackendService";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Button from "@material-ui/core/Button";
import EmailInput from "components/common/EmailInput";
import PasswordInput from "components/common/PasswordInput";
import PropTypes from "prop-types";

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
        <div>
            <h3>Login</h3>
            <form className={styles.root} onSubmit={handleFormSubmit}>
                <div>
                    <EmailInput
                        label="Email"
                        value={inputState.email}
                        onChange={event => handleInputChange("email", event)}
                        isErrorState={loginState.userNotFound}
                        helperText="User with given email doesn`t exist"
                    />
                </div>
                <div>
                    <PasswordInput
                        label="Password"
                        value={inputState.password}
                        onChange={event => handleInputChange("password", event)}
                        isErrorState={loginState.invalidPassword}
                        helperText="Wrong password"
                    />
                </div>
                <Button variant="contained" color="primary" type="submit">Submit</Button>
            </form>
        </div>
    );
}

export default LoginPage;