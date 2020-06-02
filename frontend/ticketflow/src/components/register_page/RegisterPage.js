import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import Button from "@material-ui/core/Button";
import makeStyles from "@material-ui/core/styles/makeStyles";
import TextField from "@material-ui/core/TextField";
import PasswordInput from "./PasswordInput";
import EmailInput from "./EmailInput";
import BirthdayInput from "./BirthdayInput";
import createBackendService from "../backend_service/createBackendService";

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
            updateRegisterState({submitCalled: false});
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
        // if (new Date(registerState.birthday) > Date.now) {
        //     updateRegisterState({dateInvalid: true});
        //     return;
        // } 
        await backendService.register({
            email: registerState.email,
            password: registerState.email,
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
        <div>
            <h3>RegisterPage</h3>
            <form className={styles.root} onSubmit={onSubmitAction}>
                <div>
                    <EmailInput 
                        value={registerState.email}
                        isErrorState={registerState.emailAlreadyTaken}
                        onChange={event => handleTextFieldChange("email", event)}/>
                    <TextField id="outlined-basic" label="Phone number" value={registerState.phoneNumber} onChange={event => handleTextFieldChange("phoneNumber", event)}/>
                </div>
                <div>
                    <PasswordInput
                        label="Password"
                        value={registerState.password}
                        isErrorState={registerState.passwordsDontMatch}
                        onChange={event => handleTextFieldChange("password", event)}
                    />
                    <PasswordInput
                        label="Password Again"
                        value={registerState.passwordAgain}
                        isErrorState={registerState.passwordsDontMatch}
                        onChange={event => handleTextFieldChange("passwordAgain", event)}
                    />
                </div>
                <div>
                    <BirthdayInput
                        value={registerState.birthday}
                        isErrorState={registerState.dateInvalid}
                        onChange={event => handleTextFieldChange("birthday", event)}
                    />
                </div>
                <Button variant="contained" color="primary" type="submit">Submit</Button>
            </form>
            <Button variant="contained" color="primary" className={styles.redirectButton} onClick={() => { history.push("/"); }}>Go to Home Page</Button>
        </div>
    );
}

export default RegisterPage;