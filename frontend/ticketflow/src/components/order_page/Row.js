import React from "react";
import PropTypes from "prop-types";
import Grid from "@material-ui/core/Grid";
import Seat from "components/order_page/Seat";

Row.propTypes = {
    tickets: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            buyerEmail: PropTypes.string,
            row: PropTypes.number,
            seat: PropTypes.number,
            price: PropTypes.number
        })
    ),
    isTicketAvailableFunc: PropTypes.func,
    handleClick: PropTypes.func
};

function Row(props) {
    const seatComponents = [];
    props.tickets.forEach(ticket => {
        seatComponents.push(
            <Grid item>
                <Seat
                    ticket={ticket}
                    isTicketAvailableFunc={props.isTicketAvailableFunc}
                    handleClick={props.handleClick}
                />
            </Grid>);
    });

    return (
        <Grid container spacing={1}>
            {seatComponents}
        </Grid>
    );
}

export default Row;