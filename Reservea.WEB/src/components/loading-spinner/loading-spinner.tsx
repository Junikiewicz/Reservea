import React from "react";
import { Row, Spinner } from "react-bootstrap";

const LoadingSpinner = () => (
  <Row className="justify-content-center" style={{ height: "50px" }}>
    <Spinner animation="border" role="status">
      <span className="sr-only">Ładowanie...</span>
    </Spinner>
  </Row>
);

export default LoadingSpinner;
