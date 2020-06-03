import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useProfileModel = (token) => {
    const [profileModel, setProfile] = useState(null);
    const backendService = createBackendService();

    const defaultProfileModel = {
        email: "",
        profile: {
            phoneNumber: 111,
            birthday: ""
        }
    };

    const fetchProfile = (token) => {
        backendService
            .getProfile(token)
            .then(response => response.json())
            .then(profile => setProfile(profile));
    };

    useEffect(() => {
        if (token !== null && profileModel === null) {
            fetchProfile(token);
        }
    });

    return profileModel !== null 
        ? profileModel
        : defaultProfileModel ;
};

export default useProfileModel;