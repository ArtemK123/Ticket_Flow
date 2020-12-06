import getEnvVariable from "services/utils/getEnvVariable";
import createBackendRequestSender from "services/backend_service/createBackendRequestSender";

const backendUrlBase = getEnvVariable("REACT_APP_API_GATEWAY_URL");

const createSettingsBasedBackendService = (referrer_policy) => {
    return createBackendRequestSender(referrer_policy, () => Promise.resolve(backendUrlBase));
};

export default createSettingsBasedBackendService;