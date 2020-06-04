import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const defaultProfileModel = {
    email: "default",
    profile: {
        phoneNumber: 111,
        birthday: "default"
    }
};

const useProfileModel = (token) => {
    const [profile, setProfile] = useState(undefined);

    useEffect(() => {
        if (token !== null && profile === undefined) {
            createBackendService()
                .getProfile(token)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                    return new Promise(resolve => resolve(null));
                })
                .then(fetchedProfile => {
                    setProfile(fetchedProfile);
                });
        }
    }, [token, profile]);

    if (profile) {
        return profile;
    }
    else if (profile === undefined) {
        return defaultProfileModel;
    }
    else {
        return null;
    }
};

export default useProfileModel;