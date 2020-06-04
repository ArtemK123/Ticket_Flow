import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import Button from "@material-ui/core/Button";
import makeStyles from "@material-ui/core/styles/makeStyles";
import TextField from "@material-ui/core/TextField";
import PasswordInput from "components/common/PasswordInput";
import EmailInput from "components/common/EmailInput";
import BirthdayInput from "components/register_page/BirthdayInput";
import createBackendService from "services/backend_service/createBackendService";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
    root: {
        "& > *": {
            margin: theme.spacing(1),
            width: "25ch",
        },
    },
    dateField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 200,
    },
    redirectButton: {
        marginTop: theme.spacing(3),
    }
}));

function RegisterPage() {
    const styles = useStyles();
    const history = useHistory();
    const backendService = createBackendService();

    const [registerState, changeRegisterState] = useState({
        email: "",
        phoneNumber: "",
        password: "",
        passwordAgain: "",
        birthday: "",
        submitCalled: false,
        passwordsDontMatch: false,
        emailAlreadyTaken: false,
        dateInvalid: false
    });

    useEffect(() => {
        if (registerState.submitCalled) {
            registerState.submitCalled = false;
            makeRegisterRequest();
        }
    });

    const updateRegisterState = (updatedFields) => {
        changeRegisterState(Object.assign({}, registerState, updatedFields));
    };

    const onSubmitAction = async (event) => {
        event.preventDefault();
        updateRegisterState({submitCalled: true});
    };

    const handleTextFieldChange = (targetName, event) => {
        updateRegisterState({ [targetName]: event.target.value });
    };

    const makeRegisterRequest = async() => {
        if (registerState.password !== registerState.passwordAgain) {
            updateRegisterState({passwordsDontMatch: true});
            return;
        }
        if (new Date(registerState.birthday).getTime() > new Date().getTime()) {
            updateRegisterState({dateInvalid: true});
            return;
        } 
        await backendService.register({
            email: registerState.email,
            password: registerState.password,
            profile: {
                phoneNumber: parseInt(registerState.phoneNumber),
                birthday: registerState.birthday
            }
        }).then(response => {
            if (response.ok) {
                history.push("/login");
            }
            else if (response.status === 400){
                updateRegisterState({emailAlreadyTaken: true});
            }
        });
    };

    return (
        <Box w={1}>
            <Grid container direction="row">
                <Grid item xs={1}/>
                <Grid item xs={9}>
                    <Box m={5}>
                        <form className={styles.root} onSubmit={onSubmitAction}>
                            <Grid container direction="column" spacing={2}>
                                <Grid item><Typography variant="h4">Register</Typography></Grid>
                                <Grid item>
                                    <EmailInput 
                                        value={registerState.email}
                                        label="Email"
                                        onChange={event => handleTextFieldChange("email", event)}
                                        isErrorState={registerState.emailAlreadyTaken}
                                        helperText="User with given email already exists"
                                    />
                                </Grid>
                                <Grid item>
                                    <TextField label="Phone number" value={registerState.phoneNumber} onChange={event => handleTextFieldChange("phoneNumber", event)}/>
                                </Grid>
                                <Grid item>
                                    <PasswordInput
                                        label="Password"
                                        value={registerState.password}
                                        onChange={event => handleTextFieldChange("password", event)}
                                        isErrorState={registerState.passwordsDontMatch}
                                        helperText="Passwords dont match"
                                    />
                                </Grid>
                                <Grid item>
                                    <PasswordInput
                                        label="Password Again"
                                        value={registerState.passwordAgain}
                                        onChange={event => handleTextFieldChange("passwordAgain", event)}
                                        isErrorState={registerState.passwordsDontMatch}
                                        helperText="Passwords dont match"
                                    />
                                </Grid>
                                <Grid item>
                                    <BirthdayInput
                                        value={registerState.birthday}
                                        isErrorState={registerState.dateInvalid}
                                        onChange={event => handleTextFieldChange("birthday", event)}
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

export default RegisterPage;