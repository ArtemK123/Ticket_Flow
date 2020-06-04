import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import ReadonlyTextInput from "components/common/ReadonlyTextInput";
import { Typography, Button, Grid, Box, TextField } from "@material-ui/core";
import getTimeFromDate from "services/utils/getTimeFromDate";
import createBackendService from "services/backend_service/createBackendService";

ProfilePage.propTypes = {
    token: PropTypes.string,
    profileModel: PropTypes.shape({
        userEmail: PropTypes.string,
        profile: PropTypes.shape({
            phoneNumber: PropTypes.number,
            birthday: PropTypes.string
        })
    })
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
    const [userTickets, changePageState] = useState(undefined);

    const updateUserTickets = () => {
        createBackendService()
            .getTicketsByUser(props.token)
            .then(response => response.json())
            .then(tickets => {
                changePageState(tickets);
            });
    };

    if (props.token === null) {
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
                            <Grid item container direction="column" spacing={1}>
                                <Grid item>
                                    <Button variant="contained" color="primary" onClick={updateUserTickets}>
                                        Get tickets
                                    </Button>
                                </Grid>
                                <Grid item>
                                    <TextField
                                        value={userTickets ? getTicketsValue(userTickets) : "Tickets:\n"}
                                        variant="outlined"
                                        multiline
                                        rows={10}
                                        fullWidth={true}
                                        InputProps={{
                                            readOnly: true
                                        }}
                                    />
                                </Grid>
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