import React from "react";
import PropTypes from "prop-types";
import { Container, Box, Typography } from "@material-ui/core";
import useTicketsByMovie from "services/hooks/useTicketsByMovie";

OrderPage.propTypes = {
    token: PropTypes.string,
    location: PropTypes.shape({
        state: PropTypes.shape({
            movieId: PropTypes.number
        })
    })
};

function OrderPage(props) {
    const movieId = props.location.state !== undefined ? props.location.state.movieId : -1;
    const tickets = useTicketsByMovie(movieId);

    return (
        <Box>
            <h3>OrderPage</h3>
            <h4>{movieId}</h4>
            <Container>
                <Typography>{JSON.stringify(tickets)}</Typography>
            </Container>
        </Box>
    );
}

export default OrderPage;