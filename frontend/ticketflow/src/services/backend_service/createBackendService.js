const createBackendService = () => ({
    async login(loginRequestModel) {
        return fetch("http://localhost:8080/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(loginRequestModel)
        });
    },
    async register(registerRequestModel) {
        return fetch("http://localhost:8080/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(registerRequestModel)
        });
    }
});

export default createBackendService;