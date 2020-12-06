import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import Router from "./router";
import NavbarLayout from "./components/navbar/navbar";
import "bootstrap/dist/css/bootstrap.min.css";
import "react-toastify/dist/ReactToastify.css";
import { Container } from "react-bootstrap";
import { ToastContainer } from "react-toastify";

ReactDOM.render(
  <>
    <NavbarLayout />
    <Container
      className="mt-4"
      style={{ overflow: "hidden" }}
    >
      <Router />
    </Container>
    <ToastContainer
      position="bottom-right"
      autoClose={5000}
      hideProgressBar={true}
      newestOnTop={false}
      closeOnClick
      rtl={false}
      pauseOnFocusLoss
      draggable
      pauseOnHover
    />
  </>,
  document.getElementById("root")
);
