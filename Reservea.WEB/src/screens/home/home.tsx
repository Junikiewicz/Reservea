import React from "react";
import "./home.css";
import { Row, Col, Container } from "react-bootstrap";
import ImageCarousel from "../../components/image-carousel/image-carousel";

const Home = () => (
  <Row className="justify-content-around">
    <Col xl="8" className="pageContent">
      <Container className="mt-4">
        <Row className="justify-content-center">
          <Col>
            <ImageCarousel />
          </Col>
        </Row>
        <div className="mt-4">
          <Row><h4>O NAS</h4></Row>
          <Row style={{textAlign:"justify"}}>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
            laoreet felis non volutpat molestie. Integer vestibulum nunc et nisl
            posuere volutpat. Aenean pellentesque tellus placerat rutrum
            porttitor. Sed in vehicula metus. Curabitur quis nisl eu justo
            aliquet consectetur. Duis nec orci pellentesque, maximus lacus eu,
            suscipit justo. Aenean non ornare lectus. Nam est diam, semper vel
            elementum quis, lacinia sit amet sapien. Suspendisse suscipit semper
            metus quis porta. Sed in vehicula metus. Curabitur quis nisl eu
            justo aliquet consectetur. Duis nec orci pellentesque, maximus lacus
            eu, suscipit justo. Aenean non ornare lectus. Nam est diam, semper
            vel elementum quis, lacinia sit amet sapien. Suspendisse suscipit
            semper metus quis porta.
          </Row>
        </div>
      </Container>
    </Col>
    <Col xl="3" className="pageContent" style={{whiteSpace:"pre-wrap"}} >
      <div>
        <Container>
        <Row className="mt-3"><h4>KONTAKT</h4></Row>
        <Row>
          {'45-353 Warszawa\nul. Wymyslona 23\n356567635'}
        </Row>
        </Container>
      </div>
      <div className="mt-4">
        <Container style={{textAlign:"justify"}}>
          <Row><h4>OPINIE</h4></Row>
          <Row className="mt-1">
            <span>
             Volutpat lorem  consectetur adipiscing felis ipsum dolor sit amet,elit. Phasellus
              laoreet  non volutpat molestie.
            </span>
          </Row>
          <Row className="mt-4">
          Phasellus lorem consectetur adipiscing volutpat Phasellus elit ipsum dolor sit amet,. Phasellus
            laoreet felis non volutpat molestie.
          </Row>
          <Row className="mt-4">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
            laoreet felis non volutpat molestie.
          </Row>
        </Container>
      </div>
    </Col>
  </Row>
);

export default Home;
