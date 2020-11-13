import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import Router from "./router";
import NavbarLayout from "./components/navbar/navbar";
import "bootstrap/dist/css/bootstrap.min.css";
import { Container } from "react-bootstrap";
import "./web.config";

ReactDOM.render(
  <React.StrictMode>
    <NavbarLayout />
    <Container
      className="mt-4"
      style={{ minHeight: "100vh", overflow: "hidden" }}
    >
      <Router />
    </Container>
  </React.StrictMode>,
  document.getElementById("root")
);
