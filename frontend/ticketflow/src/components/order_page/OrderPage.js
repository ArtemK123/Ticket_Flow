import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Container, Box } from "@material-ui/core";
import useTicketsByMovie from "services/hooks/useTicketsByMovie";
import Grid from "@material-ui/core/Grid";
import Seat from "components/order_page/Seat";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => ({
    ticketsContainer: {
        width: 500,
        height: 400
    },
    footerHolder: {
        height: 100
    }
}));

OrderPage.propTypes = {
    token: PropTypes.string,
    location: PropTypes.shape({
        state: PropTypes.shape({
            movieId: PropTypes.number
        })
    })
};

const groupTicketsByRow = (tickets) => {
    const rowGroupings = {};
    tickets.forEach(ticket => {
        if (rowGroupings[ticket.row] === undefined) {
            rowGroupings[ticket.row] = [ticket];
        }
        else {
            rowGroupings[ticket.row].push(ticket);
        }
    });

    return rowGroupings;
};

function OrderPage(props) {
    const styles = useStyles();
    const movieId = props.location.state !== undefined ? props.location.state.movieId : -1;
    const tickets = useTicketsByMovie(movieId);

    const [ticketsState, changeTicketsState] = useState({});

    useEffect(() => {
        if (Object.keys(ticketsState).length === 0) {
            tickets.forEach(ticket => {
                ticketsState[ticket.id] = ticket.buyerEmail === null ? "available" : "taken";
            });
            changeTicketsState(Object.assign({}, ticketsState));
        }
    });

    const handleSeatClick = (ticketId) => {
        const currentState = ticketsState[ticketId];
        if (currentState === "available") {
            ticketsState[ticketId] = "selected";
            changeTicketsState(Object.assign({}, ticketsState));
        }
        else if (currentState === "selected") {
            ticketsState[ticketId] = "available";
            changeTicketsState(Object.assign({}, ticketsState));
        }
    };

    const seatComponents = [];
    Object.values(groupTicketsByRow(tickets)).forEach((ticketsArray => {
        ticketsArray.forEach(ticket => {
            seatComponents.push(
                <Grid item>
                    <Seat
                        ticket={ticket}
                        state={ticketsState[ticket.id]}
                        handleClick={handleSeatClick}
                    />
                </Grid>
            );
        });
    }));

    return (
        <Box>
            <h3>OrderPage</h3>
            <h4>{movieId}</h4>
            <Container>
                <Grid container justify="center">
                    <Grid container item className={styles.ticketsContainer} spacing={2}>
                        {seatComponents}
                    </Grid>
                </Grid>
            </Container>
            <Box className={styles.footerHolder}></Box>
        </Box>
    );
}

export default OrderPage;