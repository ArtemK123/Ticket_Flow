import createConsulBasedBackendService from "services/backend_service/createConsulBasedBackendService";
import createSettingsBasedBackendService from "services/backend_service/createSettingsBasedBackendService";
import getEnvVariable from "services/utils/getEnvVariable";

const REFERRER_POLICY = "no-referrer-when-downgrade";

const createBackendService = () => {
    const providingType = getEnvVariable("REACT_APP_URL_PROVIDING_TYPE");

    if (providingType === "FromConsul") {
        return createConsulBasedBackendService(REFERRER_POLICY);
    }
    else if (providingType === "FromSettings") {
        return createSettingsBasedBackendService(REFERRER_POLICY);
    }
    throw new Error(`Unsupported providing type ${providingType}`);
};

export default createBackendService;