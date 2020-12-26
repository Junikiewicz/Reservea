import React from "react";
import { Form, Col, Row, Spinner } from "react-bootstrap";
import { ResourceDetailsResponse } from "../../../api/dtos/resources/resources/resourceDetailsResponse";
import { ResourceTypeForListResponse } from "../../../api/dtos/resources/resourceTypes/resourceTypeForListResponse";
import LoadingSpinner from "../../loading-spinner/loading-spinner";
import {
  getResourceStatusName,
  ResourceStatus,
} from "../../../common/enums/resourceStatus";

function EditResourceGeneral({
  resourceDetails,
  resourceTypes,
  register,
  onResourceTypeIdChange,
}: any) {
  if (!resourceDetails) {
    return <LoadingSpinner />;
  }

  return (
    <Form>
      <Row>
        <Col>
          <Form.Group>
            <Form.Label>Nazwa</Form.Label>
            <Form.Control
              id="name"
              name="name"
              ref={register}
              disabled={
                resourceDetails?.resourceStatusId == ResourceStatus.Removed
              }
              className="bg-dark text-light"
              type="text"
              defaultValue={resourceDetails.name}
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Opis</Form.Label>
            <Form.Control
              id="description"
              name="description"
              ref={register}
              disabled={
                resourceDetails?.resourceStatusId == ResourceStatus.Removed
              }
              className="bg-dark text-light"
              as="textarea"
              defaultValue={resourceDetails.description}
              rows={4}
            />
          </Form.Group>
        </Col>
        <Col>
          <Form.Group>
            <Form.Label>Cena za godzinę</Form.Label>
            <Form.Control
              id="pricePerHour"
              name="pricePerHour"
              ref={register}
              disabled={
                resourceDetails?.resourceStatusId == ResourceStatus.Removed
              }
              className="bg-dark text-light"
              type="number"
              defaultValue={resourceDetails.pricePerHour}
            />
          </Form.Group>
          <Form.Group>
            <Form.Label>Typ zasobu</Form.Label>
            <Form.Control
              id="resourceTypeId"
              name="resourceTypeId"
              onChange={onResourceTypeIdChange}
              disabled={
                resourceDetails?.resourceStatusId == ResourceStatus.Removed
              }
              defaultValue={resourceDetails.resourceTypeId}
              ref={register}
              className="bg-dark text-light"
              as="select"
            >
              {resourceTypes.map((element: ResourceTypeForListResponse) => (
                <option key={element.id} value={element.id}>
                  {element.name}
                </option>
              ))}
            </Form.Control>
          </Form.Group>
          <Form.Group>
            <Form.Label>Status zasobu</Form.Label>
            <Form.Control
              id="resourceStatusId"
              name="resourceStatusId"
              disabled={
                resourceDetails?.resourceStatusId == ResourceStatus.Removed
              }
              defaultValue={resourceDetails.resourceStatusId}
              ref={register}
              className="bg-dark text-light"
              as="select"
            >
              {Object.keys(ResourceStatus)
                .filter((k) => !isNaN(Number(k)))
                .map((key: string) => (
                  <option
                    key={key}
                    disabled={parseInt(key) === ResourceStatus.Removed}
                    aria-selected="true"
                    value={parseInt(key)}
                  >
                    {getResourceStatusName(parseInt(key))}
                  </option>
                ))}
            </Form.Control>
          </Form.Group>
        </Col>
      </Row>
    </Form>
  );
}

export default EditResourceGeneral;
