import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import ReadonlyTextInput from "components/common/ReadonlyTextInput";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import { Typography } from "@material-ui/core";

ProfilePage.propTypes = {
    isUserLoggedIn: PropTypes.bool,
    profileModel: PropTypes.shape({
        userEmail: PropTypes.string,
        profile: PropTypes.shape({
            phoneNumber: PropTypes.number,
            birthday: PropTypes.string
        })
    }),
    tickets: PropTypes.arrayOf(PropTypes.object)
};

function ProfilePage(props) {
    const getTicketsValue = (tickets) => {
        if (tickets.length === 0){
            return "";
        }
        return tickets.reduce((accumulator, ticket) => accumulator + JSON.stringify(ticket) + "\n\n");
    };

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
                <Grid item xs={1}/>
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
                                    isMultiline={false}
                                />
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Phone number"
                                    value={props.profileModel.profile.phoneNumber}
                                    isMultiline={false}
                                />
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Birthday"
                                    value={props.profileModel.profile.birthday}
                                    isMultiline={false}
                                />
                            </Grid>
                            <Grid item>
                                <ReadonlyTextInput
                                    label="Tickets"
                                    value={getTicketsValue(props.tickets)}
                                    isMultiline={true}
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