const getEnvVariable = (variable) => {
    // eslint-disable-next-line no-undef
    const value = process.env[variable];
    if (value === undefined) {
        throw new Error(`Can't read value of the environment variable ${variable}`);
    }
    return value;
};

export default getEnvVariable;