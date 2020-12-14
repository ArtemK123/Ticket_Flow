import { useState, useEffect } from "react";
import createBackendServiceAsync from "services/backend_service/createBackendServiceAsync";

const defaultProfileModel = {
    email: "default@gmail.com",
    profile: {
        phoneNumber: 111,
        birthday: "1900-01-01T00:00:00"
    }
};

const useProfileModel = (token) => {
    const [profile, setProfile] = useState(undefined);

    useEffect(() => {
        if (token !== null) {
            createBackendServiceAsync()
                .then(backendService => backendService.getProfile(token))
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
    }, [token]);

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