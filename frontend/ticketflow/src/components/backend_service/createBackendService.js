const createBackendService = () => ({
    login() {
        return "testValue";
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