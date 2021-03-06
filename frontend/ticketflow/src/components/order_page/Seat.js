import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import PropTypes from "prop-types";
import SeatTitle from "./SeatTitle";

Seat.propTypes = {
    ticket: PropTypes.shape({
        id: PropTypes.number,
        buyerEmail: PropTypes.string,
        row: PropTypes.number,
        seat: PropTypes.number,
        price: PropTypes.number
    }),
    state: PropTypes.string,
    handleClick: PropTypes.func
};

const useStyles = makeStyles(() => ({
    availablePaper: {
        height: 50,
        width: 70,
        "user-select": "none",
        backgroundColor: "white",
        "&:hover": {
            backgroundColor: "Azure"
        }
    },
    selectedPaper: {
        height: 50,
        width: 70,
        "user-select": "none",
        backgroundColor: "red",
    },
    takenPaper: {
        height: 50,
        width: 70,
        "user-select": "none",
        backgroundColor: "grey"
    },
    brokenPaper: {
        height: 50,
        width: 70,
        "user-select": "none",
        backgroundColor: "blue"
    }
}));

function Seat(props) {
    const styles = useStyles();

    if (props.state === "available") {
        return (
            <Paper className={styles.availablePaper} onClick={() => props.handleClick(props.ticket.id)}>
                <SeatTitle value={`${props.ticket.row}, ${props.ticket.seat}`}/>
            </Paper>
        );
    }

    else if (props.state === "selected") {
        return (
            <Paper className={styles.selectedPaper} onClick={() => props.handleClick(props.ticket.id)}>
                <SeatTitle value={`${props.ticket.row}, ${props.ticket.seat}`}/>
            </Paper>
        );
    }
    
    else if (props.state === "taken") {
        return (
            <Paper className={styles.takenPaper}>
                <SeatTitle value={`${props.ticket.row}, ${props.ticket.seat}`}/>
            </Paper>
        );
    }
    else {
        return (
            <Paper className={styles.brokenPaper}>
                <SeatTitle value={"error"}/>
            </Paper>
        );
    }
}

export default Seat;