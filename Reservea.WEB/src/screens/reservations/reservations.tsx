import React, { useEffect, useState } from "react";
import ResourceTypeTimeline from "../resource-type-timeline/resource-type-timeline";
import WorkInProgress from "../../components/work-in-progress/work-in-progress";
import { ResourceTypeWithDetailsForListResponse } from "../../api/dtos/resources/resourceTypes/resourceTypeWithDetailsForListResponse";
import { resourcesTypesWithDetailsListRequest } from "../../api/clients/resourcesClient";
import { Accordion, Button, Card, Col, Container, Row } from "react-bootstrap";

function Reservations() {
  const [resourceTypes, setResourceTypes] = useState<
    Array<ResourceTypeWithDetailsForListResponse>
  >([]);

  useEffect(() => {
    resourcesTypesWithDetailsListRequest()
      .then((response: Array<ResourceTypeWithDetailsForListResponse>) => {
        setResourceTypes(response);
      })
      .catch(() => {});
  }, []);

  return (
    <div>
      <div className="pageHeader text-center">
        <Container className="my-3">
          <h2>Wybierz typ zasobu</h2>
        </Container>
      </div>
      <Container className="mt-4">
        <Accordion>
          {resourceTypes.map((element, index) => (
            <Card bg="dark" className="mt-4">
              <Accordion.Toggle
                as={Card.Header}
                className="text-center"
                eventKey={index.toString()}
              >
                {element.name}
              </Accordion.Toggle>
              <Accordion.Collapse eventKey={index.toString()}>
                <Card.Body>
                  <Row>
                    <Col className="col-10">{element.description}</Col>
                    <Col className="col-2">
                      <Button
                        variant={"success"}
                        href={"createReservation/" + element.id}
                      >
                        Przejdź do osi
                      </Button>
                    </Col>
                  </Row>
                </Card.Body>
              </Accordion.Collapse>
            </Card>
          ))}
        </Accordion>
      </Container>
      <div className="pageContent mt-4"></div>
    </div>
  );
}

export default Reservations;
