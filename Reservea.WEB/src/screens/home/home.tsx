import React, { useEffect, useState } from "react";
import "./home.css";
import { Row, Col, Container, Carousel, Button } from "react-bootstrap";
import {
  addUserRateRequest,
  getAllImages,
  getAllTextFieldsContentsRequest,
  getUserRatesForHomepageRequest,
  Photo,
  TextFieldContent,
} from "../../api/clients/cmsClients";
import AddUserRateModal from "./add-user-rate-modal";
import { toast } from "react-toastify";

function Home() {
  const [textFieldsContents, setTextFieldsContents] = useState<
    Array<TextFieldContent>
  >([]);

  const [showEditAttribute, setShowEditAttribute] = useState(false);
  const handleCloseEditAttribute = () => setShowEditAttribute(false);
  const handleChooseEditAttribute = (reqest: string) => {
    addUserRateRequest(reqest)
      .then(() => {
        toast.success("Dziękujemy za twoją opinie!");
      })
      .catch(() => {});
    setShowEditAttribute(false);
  };
  const handleShowEditAtribute = () => {
    setShowEditAttribute(true);
  };

  const [pictures, setPictures] = useState<Array<any>>([]);

  const [userRates, setUserRates] = useState<Array<any>>([]);

  useEffect(() => {
    getUserRatesForHomepageRequest()
      .then((response: Array<any>) => {
        setUserRates(response);
      })
      .catch(() => {});
    getAllTextFieldsContentsRequest()
      .then((response: Array<TextFieldContent>) => {
        setTextFieldsContents(response);
      })
      .catch(() => {});
    getAllImages()
      .then((response: Array<Photo>) => {
        setPictures(response);
      })
      .catch(() => {});
  }, []);

  return (
    <Row className="justify-content-around">
      <Col xl="8" className="pageContent">
        <Container className="mt-4">
          <Row className="justify-content-center">
            <Col>
              <Carousel className="my-carousel">
                {pictures.map((element) => (
                  <Carousel.Item>
                    <img
                      className="d-block w-100"
                      src={element.url}
                      alt="First slide"
                      style={{ height: "400px" }}
                    />
                  </Carousel.Item>
                ))}
              </Carousel>
            </Col>
          </Row>
          <div className="mt-4">
            <Row>
              <h4>O NAS</h4>
            </Row>
            <Row style={{ textAlign: "justify" }}>
              {textFieldsContents.find((x) => x.name === "aboutUs")?.content}
            </Row>
          </div>
        </Container>
      </Col>
      <Col xl="3" className="pageContent" style={{ whiteSpace: "pre-wrap" }}>
        <div className="mt-4">
          <Container style={{ textAlign: "justify" }}>
            <Row>
              <h4>OPINIE</h4>
              <Button onClick={handleShowEditAtribute} className="ml-auto" variant="success">
                Dodaj własną!
              </Button>
            </Row>
            {userRates.map((element) => (
              <Row className="mt-1 mb-3">
                <span>{element.feedback}</span>
              </Row>
            ))}
          </Container>
          <div>
            <Container>
              <Row className="mt-3">
                <h4>KONTAKT</h4>
              </Row>
              <Row>
                {textFieldsContents.find((x) => x.name === "contact")?.content}
              </Row>
            </Container>
          </div>
        </div>
      </Col>
      <AddUserRateModal
        show={showEditAttribute}
        handleClose={handleCloseEditAttribute}
        handleChoose={handleChooseEditAttribute}
      />
    </Row>
  );
}

export default Home;
