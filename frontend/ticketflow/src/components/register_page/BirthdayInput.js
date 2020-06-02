import React from "react";
import PropTypes from "prop-types";
import TextField from "@material-ui/core/TextField";
import makeStyles from "@material-ui/core/styles/makeStyles";

BirthdayInput.propTypes = {
    isErrorState: PropTypes.bool,
    value: PropTypes.string,
    onChange: PropTypes.func,
};

const useStyles = makeStyles((theme) => ({
    dateField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 200,
    }
}));

function BirthdayInput(props) {
    const styles = useStyles();

    if (props.isErrorState) {
        return (
            <TextField
                id="date"
                label="Birthday"
                value={props.value}
                type="date"
                className={styles.dateField}
                onChange={props.onChange}
                InputLabelProps={{
                    shrink: true,
                }}
                error
                helperText="Invalid birthday date"
            />
        );
    }
    return (
        <TextField
            id="date"
            label="Birthday"
            value={props.value}
            type="date"
            className={styles.dateField}
            onChange={props.onChange}
            InputLabelProps={{
                shrink: true,
            }}
        />
    );
}

export default BirthdayInput;