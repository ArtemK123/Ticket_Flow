import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import ReadonlyTextInput from "components/common/ReadonlyTextInput";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";
import getTimeFromDate from "services/utils/getTimeFromDate";
import TextField from "@material-ui/core/TextField";

ProfilePage.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    profileModel: PropTypes.shape({
        userEmail: PropTypes.string,
        profile: PropTypes.shape({
            phoneNumber: PropTypes.number,
            birthday: PropTypes.string
        })
    }),
    tickets: PropTypes.arrayOf(PropTypes.shape({
        id: PropTypes.number,
        buyerEmail: PropTypes.string,
        row: PropTypes.number,
        seat: PropTypes.number,
        price: PropTypes.number,
        movie: PropTypes.shape({
            id: PropTypes.number,
            title: PropTypes.string,
            startTime: PropTypes.instanceOf(Date),
            cinemaHallName: PropTypes.string
        })
    }))
};

const formatDate = (date) => {
    const dateFormat = { day: "numeric", month: "numeric", year: "numeric"};
    const day = date.toLocaleString("uk-UA", dateFormat);
    const time = getTimeFromDate(date);

    return `${day} ${time}`;
};

const formatTicketValue = (ticket) => {
    return `${ticket.movie.title}: ${formatDate(new Date(ticket.movie.startTime))}, ${ticket.movie.cinemaHallName} (row ${ticket.row}, seat ${ticket.seat})`;
};

const getTicketsValue = (tickets) => {
    let ticketsInputValue = "Tickets:\n";
    tickets.forEach(ticket => {
        ticketsInputValue = ticketsInputValue + formatTicketValue(ticket) + "\n";
    });
    return ticketsInputValue;
};

function ProfilePage(props) {
    if (!props.isUserLoggedIn) {
        return (
            <div>
                <p>You should log in if you want to see your profile</p>
                <Link to="/login">Go to login page</Link>
            </div>
        );
    }

    return (
        <Box w={1}>
            <Grid container direction="row">
                <Grid item xs={1}>
                </Grid>
                <Grid item xs={6}>
                    <Box m={5}>
                        <Grid container direction="column" spacing={2}>
                            <Grid item>
                                <Typography variant="h4">Your profile</Typography>
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Email"
                                    value={props.profileModel.userEmail}
                                />
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Phone number"
                                    value={`${props.profileModel.profile.phoneNumber}`}
                                />
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Birthday"
                                    value={props.profileModel.profile.birthday}
                                />
                            </Grid>
                            <Grid item>
                                <TextField
                                    defaultValue={getTicketsValue(props.tickets)}
                                    variant="outlined"
                                    multiline
                                    rowsMax={10}
                                    fullWidth={true}
                                    InputProps={{
                                        readOnly: true
                                    }}
                                />
                            </Grid>
                        </Grid>
                    </Box>
                </Grid>
                <Grid item xs={5}/>
            </Grid>
        </Box>
    );
}

export default ProfilePage;