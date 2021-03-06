import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Container, Box, Typography } from "@material-ui/core";
import useTicketsByMovie from "services/hooks/useTicketsByMovie";
import Grid from "@material-ui/core/Grid";
import Seat from "components/order_page/Seat";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import getTimeFromDate from "services/utils/getTimeFromDate";
import createBackendServiceAsync from "services/backend_service/createBackendServiceAsync";
import { Link } from "react-router-dom";

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
    }),
    logoutCallback: PropTypes.func
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

    const [pageState, changePageState] = useState({
        ticketsState: {},
        makeOrderCalled: false
    });

    useEffect(() => {
        if (Object.keys(pageState.ticketsState).length === 0 && tickets.length > 0) {
            const newTicketsState = {};
            tickets.forEach(ticket => {
                newTicketsState[ticket.id] = ticket.buyerEmail === null ? "available" : "taken";
            });

            changePageState(Object.assign({}, pageState, {ticketsState: newTicketsState}));
        }
    }, [pageState, tickets]);

    useEffect(() => {
        if (pageState.makeOrderCalled) {
            const makeOrder = async () => {
                const backendService = await createBackendServiceAsync();
                const newTicketsState = Object.assign({}, pageState.ticketsState);

                for (let ticketId in newTicketsState) {
                    const numericTicketId = parseInt(ticketId, 10);

                    if (newTicketsState[ticketId] === "selected" && numericTicketId > 0) {
                        const response = await backendService.order({
                            ticketId: numericTicketId,
                            token: props.token
                        });
                        if (response.status === 401) {
                            props.logoutCallback();
                        }
                        newTicketsState[ticketId] = response.status === 202 || response.status === 200 ? "taken" : "error";
                    }
                }
                changePageState(Object.assign({}, pageState, {
                    ticketsState: newTicketsState,
                    makeOrderCalled: false
                }));
            };

            makeOrder();
        }
    });

    const handleSeatClick = (ticketId) => {
        const currentState = pageState.ticketsState[ticketId];
        if (currentState === "available") {
            const newTicketsState = Object.assign({}, pageState.ticketsState);
            newTicketsState[ticketId] = "selected";
            changePageState(Object.assign({}, pageState, {ticketsState: newTicketsState}));
        }
        else if (currentState === "selected") {
            const newTicketsState = Object.assign({}, pageState.ticketsState);
            newTicketsState[ticketId] = "available";
            changePageState(Object.assign({}, pageState, {ticketsState: newTicketsState}));
        }
    };

    const seatComponents = [];
    Object.values(groupTicketsByRow(tickets)).forEach((ticketsArray => {
        const ticketsSortedBySeatNumber = ticketsArray.sort((a, b) => a.seat - b.seat);
        
        const seatComponentsInRow = [];
        ticketsSortedBySeatNumber.forEach(ticket => {
            seatComponentsInRow.push(
                <Grid item key={"grid" + ticket.id}>
                    <Seat
                        key={ticket.id}
                        ticket={ticket}
                        state={pageState.ticketsState[ticket.id]}
                        handleClick={handleSeatClick}
                    />
                </Grid>
            );
        });
        seatComponents.push(<Grid container item justify="center" spacing={2} key={JSON.stringify(ticketsArray)}>{seatComponentsInRow}</Grid>);
    }));

    if (props.token === null) {
        return (
            <Box m={5}>
                <Typography variant="h6">You should log in if you want to order tickets</Typography>
                <ul>
                    <li><Link to="/login">Go to login page</Link></li>
                </ul>
            </Box>
        );
    }

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
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => changePageState(Object.assign({}, pageState, {makeOrderCalled: true}))}
                                >
                                    Order tickets
                                </Button>
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