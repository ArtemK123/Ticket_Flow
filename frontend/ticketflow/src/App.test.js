/* eslint-disable no-undef */
import React from "react";
import App from "./App";

test("renders without crush", () => {
    const div = document.createElement("div");
    ReactDOM.render(<App />, div);
    ReactDOM.unmountComponentAtNode(div);
});
