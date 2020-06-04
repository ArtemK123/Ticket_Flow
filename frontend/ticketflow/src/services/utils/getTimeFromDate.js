const getTimeFromDate = (date) => {
    let hours = date.getHours();
    hours = hours > 9 ? hours : "0" + hours;

    let minutes = date.getMinutes();
    minutes = minutes > 9 ? minutes : "0" + minutes;

    return `${hours}:${minutes}`;
};

export default getTimeFromDate;