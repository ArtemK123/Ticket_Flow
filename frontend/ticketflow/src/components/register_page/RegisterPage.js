import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import Button from "@material-ui/core/Button";
import makeStyles from "@material-ui/core/styles/makeStyles";
import TextField from "@material-ui/core/TextField";

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
    const [registerState, changeRegisterState] = useState({
        textFieldValues: {
            email: "",
            phoneNumber: "",
            password: "",
            passwordAgain: "",
            birthday: new Date()
        },
        passwordsDontMatch: false
    });

    const onSubmitAction = () => {
        const message = JSON.stringify(registerState.textFieldValues);

        alert(message);
    };

    const handleTextFieldChange = (targetName, event) => {
        registerState.textFieldValues[targetName] = event.target.value;
        changeRegisterState(registerState);
    };

    return (
        <div>
            <h3>RegisterPage</h3>
            <form className={styles.root} onSubmit={onSubmitAction}>
                <div>
                    <TextField id="outlined-basic" label="Email" onChange={event => handleTextFieldChange("email", event)}/>
                    <TextField id="outlined-basic" label="Phone number" onChange={event => handleTextFieldChange("phoneNumber", event)}/>
                </div>
                <div>
                    <TextField id="outlined-basic" type="password" label="Password" onChange={event => handleTextFieldChange("password", event)}/>
                    <TextField id="outlined-basic" type="password" label="Password again" onChange={event => handleTextFieldChange("passwordAgain", event)}/>
                </div>
                <div>
                    <TextField
                        id="date"
                        label="Birthday"
                        type="date"
                        defaultValue="2020-01-20"
                        className={styles.dateField}
                        onChange={event => handleTextFieldChange("birthday", event)}
                        InputLabelProps={{
                            shrink: true,
                        }}
                    />
                </div>
                <Button variant="contained" color="primary" type="submit">Submit</Button>
            </form>
            <Button variant="contained" color="primary" className={styles.redirectButton} onClick={() => { history.push("/"); }}>Go to Home Page</Button>
        </div>
    );
}

export default RegisterPage;