import * as createConsulServiceExternal from "consul";
import getEnvVariable from "services/utils/getEnvVariable";
import createBackendRequestSender from "services/backend_service/createBackendRequestSender";

const createConsulService = () => {
    const consulUrl = getEnvVariable("REACT_APP_CONSUL_URL");
    const consulUrlSplitted = consulUrl.split(":");
    const consulHost = `${consulUrlSplitted[0]}:${consulUrlSplitted[1]}`;
    const consulPort = consulUrlSplitted[2];
    return createConsulServiceExternal({ host: consulHost, port: consulPort });
};

const consulService = createConsulService();

const createConsulBasedBackendServiceAsync = async (referrer_policy) => { 
    return createBackendRequestSender(referrer_policy, async () => await getApiGatewayUrlAsync(consulService));
};

const getApiGatewayUrlAsync = (consul) => {
    return new Promise((resolve, reject) => {
        consul.agent.service.list((err, services) => {
            if (err || services === undefined) {
                reject(err);
            }

            const apiGatewayServiceName = getEnvVariable("REACT_APP_API_GATEWAY_SERVICE_NAME");
            const apiGatewayService = findApiGatewayService(services, apiGatewayServiceName);
            resolve(`${apiGatewayService.Address}:${apiGatewayService.Port}`);
        });
    });
};

const findApiGatewayService = (services, apiGatewayServiceName) => {
    for (let service of Object.keys(services)) {
        if (service.Service === apiGatewayServiceName) {
            return service;
        }
    }
};

export default createConsulBasedBackendServiceAsync;