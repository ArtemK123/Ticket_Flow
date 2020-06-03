import { useState, useEffect } from "react";
import createBackendService from "services/backend_service/createBackendService";

const useProfileModel = (token) => {
    const [response, setResponse] = useState(null);
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
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                return new Promise(() => null, () => null);
            })
            .then(fetchedProfile => {
                if (fetchedProfile !== null) {
                    setResponse( {
                        profileModel: fetchedProfile,
                        success: true
                    });
                }
                else {
                    setResponse({
                        profileModel: defaultProfileModel,
                        success: false
                    });
                }
            });
    };

    useEffect(() => {
        if (token !== null && response === null) {
            fetchProfile(token);
        }
    });

    if (response === null) {
        return {
            profileModel: defaultProfileModel,
            success: null
        };
    }
    return response;
};

export default useProfileModel;