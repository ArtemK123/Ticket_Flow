import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Container, Box, Typography } from "@material-ui/core";
import useTicketsByMovie from "services/hooks/useTicketsByMovie";
import Grid from "@material-ui/core/Grid";
import Seat from "components/order_page/Seat";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import getTimeFromDate from "services/utils/getTimeFromDate";

const useStyles = makeStyles(() => ({
    footerHolder: {
        height: 100
    },
    titleHolder: {
        height: 80
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
    }, [ticketsState, tickets]);

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

    const makeOrder = () => {

    };

    const seatComponents = [];
    Object.values(groupTicketsByRow(tickets)).forEach((ticketsArray => {
        const seatComponentsInRow = [];
        ticketsArray.forEach(ticket => {
            seatComponentsInRow.push(
                <Grid item>
                    <Seat
                        key={ticket.id}
                        ticket={ticket}
                        state={ticketsState[ticket.id]}
                        handleClick={handleSeatClick}
                    />
                </Grid>
            );
        });
        seatComponents.push(<Grid container item justify="center" spacing={2}>{seatComponentsInRow}</Grid>);
    }));

    return (
        <Box w={1}>
            <Grid container direction="row">
                <Grid item xs={3}>
                    <Box m={5}>
                        <Typography variant="h5">{tickets.length > 0 ? tickets[0].movie.title : ""}</Typography>
                        <Typography>Place: {tickets.length > 0 ? tickets[0].movie.cinemaHallName : ""}</Typography>
                        <Typography>Time: {tickets.length > 0 ? getTimeFromDate(new Date(tickets[0].movie.startTime)) : ""}</Typography>
                        <Box mt={2}>
                            <Typography>Tickets:</Typography>
                            <Typography> - white: available</Typography>
                            <Typography> - red: selected</Typography>
                            <Typography> - grey: taken</Typography>
                            <Typography>Token: {props.token}</Typography>
                        </Box>
                    </Box>
                </Grid>
                <Grid item xs={6}>
                    <Box className={styles.titleHolder}/>
                    <Container>
                        <Grid container alignItems="center" direction="column" spacing={5}>
                            <Grid item>
                                <Typography>Screen</Typography>
                            </Grid>
                            <Grid container item className={styles.ticketsContainer} spacing={2}>
                                {seatComponents}
                            </Grid>
                            <Grid item>
                                <Button variant="contained" color="primary" onClick={makeOrder}>Order tickets</Button>
                            </Grid>
                        </Grid>
                    </Container>
                </Grid>
                <Grid item xs={3}>
                    <Box className={styles.footerHolder}/>
                </Grid> 
            </Grid>
        </Box>
    );
}

export default OrderPage;