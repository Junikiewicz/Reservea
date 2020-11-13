import React from "react";
import "./home.css";
import { Row, Col } from "react-bootstrap";
import ImageCarousel from "../../components/image-carousel/image-carousel";

const Home = () => (
  <Row className="justify-content-around">
    <Col xl="8">
      <Row>
        <ImageCarousel />
      </Row>
      <div className="mt-4">
        <Row>O NAS</Row>
        <Row>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
          laoreet felis non volutpat molestie. Integer vestibulum nunc et nisl
          posuere volutpat. Aenean pellentesque tellus placerat rutrum
          porttitor. Sed in vehicula metus. Curabitur quis nisl eu justo aliquet
          consectetur. Duis nec orci pellentesque, maximus lacus eu, suscipit
          justo. Aenean non ornare lectus. Nam est diam, semper vel elementum
          quis, lacinia sit amet sapien. Suspendisse suscipit semper metus quis
          porta. Sed in vehicula metus. Curabitur quis nisl eu justo aliquet
          consectetur. Duis nec orci pellentesque, maximus lacus eu, suscipit
          justo. Aenean non ornare lectus. Nam est diam, semper vel elementum
          quis, lacinia sit amet sapien. Suspendisse suscipit semper metus quis
          porta.
        </Row>
      </div>
    </Col>
    <Col xl="3">
      <div>
        <Row>OPINIE</Row>
        <Row className="mt-1">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
          laoreet felis non volutpat molestie.
        </Row>
        <Row className="mt-4">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
          laoreet felis non volutpat molestie.
        </Row>
        <Row className="mt-4">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
          laoreet felis non volutpat molestie.
        </Row>
      </div>
      <div className="mt-4">
        <Row>KONTAKT</Row>
        <Row>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus
          laoreet felis non volutpat molestie. Integer vestibulum nunc et nisl
          posuere volutpat.
        </Row>
      </div>
    </Col>
  </Row>
);

export default Home;
