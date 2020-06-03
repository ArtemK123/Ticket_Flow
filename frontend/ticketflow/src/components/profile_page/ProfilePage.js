import React from "react";
import { TextField } from "@material-ui/core";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";

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
        <div>
            <h3>Your profile</h3>
            <div>
                <TextField
                    label="Email"
                    defaultValue={props.profileModel.userEmail}
                    variant="outlined"
                    InputProps={{
                        readOnly: true
                    }}
                />
            </div>
            <div>
                <TextField
                    label="Phone number"
                    defaultValue={props.profileModel.profile.phoneNumber}
                    variant="outlined"
                    InputProps={{
                        readOnly: true
                    }}
                />
            </div>
            <div>
                <TextField
                    label="Birthday"
                    defaultValue={props.profileModel.profile.birthday}
                    variant="outlined"
                    InputProps={{
                        readOnly: true
                    }}
                />
            </div>
            <div>
                <TextField
                    label="Tickets"
                    defaultValue={getTicketsValue(props.tickets)}
                    variant="outlined"
                    multiline
                    InputProps={{
                        readOnly: true
                    }}
                />
            </div>
        </div>
    );
}

export default ProfilePage;