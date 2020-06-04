import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import ReadonlyTextInput from "components/common/ReadonlyTextInput";

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
                <ReadonlyTextInput
                    label="Email"
                    value={props.profileModel.userEmail}
                    isMultiline={false}
                />
            </div>
            <div>
                <ReadonlyTextInput
                    label="Phone number"
                    value={props.profileModel.profile.phoneNumber}
                    isMultiline={false}
                />
            </div>
            <div>
                <ReadonlyTextInput
                    label="Birthday"
                    value={props.profileModel.profile.birthday}
                    isMultiline={false}
                />
            </div>
            <div>
                <ReadonlyTextInput
                    label="Tickets"
                    value={getTicketsValue(props.tickets)}
                    isMultiline={true}
                />
            </div>
        </div>
    );
}

export default ProfilePage;