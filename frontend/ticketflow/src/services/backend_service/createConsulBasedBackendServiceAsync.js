import createBackendRequestSender from "services/backend_service/createBackendRequestSender";

const createConsulBasedBackendServiceAsync = async (referrer_policy) => { 
    return createBackendRequestSender(referrer_policy, async () => await getApiGatewayUrlAsync());
};

const getApiGatewayUrlAsync = (_) => {
    throw new Error("Consul service discovery is not yet implemented");
};

export default createConsulBasedBackendServiceAsync;