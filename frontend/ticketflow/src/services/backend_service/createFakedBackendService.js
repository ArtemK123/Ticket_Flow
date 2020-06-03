const createPromiseWithReturn = (objectToReturn) => new Promise((resolve) => resolve(objectToReturn));

const createFakedBackendService = () => ({
    async login() {
        return createPromiseWithReturn({
            ok: true,
            status: 203,
            text: () => createPromiseWithReturn("jwt-token")
        });
    },
    async register() {
        return createPromiseWithReturn({
            ok: true,
            status: 201
        });
    },
    async logout() {
        return createPromiseWithReturn({
            ok: true,
            status: 203
        });
    },
    async getProfile() {
        return createPromiseWithReturn({
            ok: true,
            status: 200,
            json: () => createPromiseWithReturn({
                email: "testemail@gmail.com",
                profile: {
                    phoneNumber: 380971234567,
                    birthday: "2020-03-20"
                }
            })
        });
    },
    async getTicketsByUser() {
        return createPromiseWithReturn({
            ok: true,
            status: 200,
            json: () => createPromiseWithReturn([])
        });
    },
    async getMovies() {
        return createPromiseWithReturn({
            ok: true,
            status: 200,
            json: () => createPromiseWithReturn([])
        });
    }
});

export default createFakedBackendService;