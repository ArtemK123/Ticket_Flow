import * as createConsulServiceExternal from "consul";
import getEnvVariable from "services/utils/getEnvVariable";
import createBackendRequestSender from "services/backend_service/createBackendRequestSender";

const createConsulService = () => {
    const consulUrl = getEnvVariable("REACT_APP_CONSUL_URL");
    const consulHostAndPort = consulUrl.split(":");
    return createConsulServiceExternal({ host: consulHostAndPort[0], port: consulHostAndPort[1], promisify: true });
};

const consulService = createConsulService();

const createConsulBasedBackendServiceAsync = async (referrer_policy) => { 
    return createBackendRequestSender(referrer_policy, async () => await getApiGatewayUrlAsync(consulService));
};

const getApiGatewayUrlAsync = async (consul) => {
    const services = await consul.agent.service.list;

    const apiGatewayServiceName = getEnvVariable("REACT_APP_API_GATEWAY_SERVICE_NAME");
    const apiGatewayService = findApiGatewayService(services, apiGatewayServiceName);
    return `${apiGatewayService.Address}:${apiGatewayService.Port}`;
};

const findApiGatewayService = (services, apiGatewayServiceName) => {
    for (let service of Object.keys(services)) {
        if (service.Service === apiGatewayServiceName) {
            return service;
        }
    }
};

export default createConsulBasedBackendServiceAsync;