import React, { useEffect, useState } from "react";
import {
  Button,
  Row,
  Tab,
  Nav,
  Col,
  Container,
  Form,
  Table,
} from "react-bootstrap";
import {
  createResourceTypeRequest,
  resourcesTypeDetailsRequest,
  updateResourceTypeRequest,
} from "../../api/clients/resourcesClient";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faLongArrowAltLeft,
  faTrashAlt,
} from "@fortawesome/free-solid-svg-icons";
import { Link, useHistory } from "react-router-dom";
import { ResourceTypeDetailsResponse } from "../../api/dtos/resources/resourceTypes/resourceTypeDetailsResponse";
import { UpdateResourceTypeFormData } from "../../common/models/forms/updateResourceTypeFormData";
import { ResourceTypeAttributeResponse } from "../../api/dtos/resources/resourceTypeAttributes/resourceTypeAttributeResponse";
import AddAttributeModal from "../edit-resource-type/add-attribute-modal";
import { AttributeForListResponse } from "../../api/dtos/resources/attributes/attributeForListResponse";
import { AddResourceTypeResponse } from "../../api/dtos/resources/resourceTypes/addResourceTypeResponse";

function AddResourceType(props: any) {
  const history = useHistory();
  const [
    resourceTypeDetails,
    setResourceTypeDetails,
  ] = useState<ResourceTypeDetailsResponse>();
  const {
    register,
    handleSubmit,
    reset,
    setValue,
    formState,
  } = useForm<UpdateResourceTypeFormData>({
    mode: "onSubmit",
  });

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleChoose = (selectedValue: AttributeForListResponse) => {
    insert(selectedValue);
    setShow(false);
  };
  const handleShow = () => setShow(true);

  useEffect(() => {
    setResourceTypeDetails({
      id: 0,
      name: "",
      description: "",
      resourceTypeAttributes: [],
      isDeleted: false
    });
    register({ name: "resourceTypeAttributes", type: "custom" });
    setValue("resourceTypeAttributes", [], { shouldDirty: false });
  }, []);

  const insert = (attribute: AttributeForListResponse) => {
    if (resourceTypeDetails) {
      const newDetails = { ...resourceTypeDetails };
      newDetails.resourceTypeAttributes.push({
        attributeId: attribute.id,
        resourceTypeId: props.match.params.id,
        name: attribute.name,
      });
      setResourceTypeDetails(newDetails);
      setValue("resourceTypeAttributes", newDetails.resourceTypeAttributes, {
        shouldDirty: true,
      });
    }
  };

  const remove = (id: number) => {
    if (resourceTypeDetails) {
      const attributes = resourceTypeDetails.resourceTypeAttributes.filter(
        (x) => x.attributeId != id
      );
      const newDetails = { ...resourceTypeDetails };
      newDetails.resourceTypeAttributes = attributes;
      setResourceTypeDetails(newDetails);
      setValue("resourceTypeAttributes", attributes, { shouldDirty: true });
    }
  };

  const onSubmit = async (data: UpdateResourceTypeFormData): Promise<void> => {
    createResourceTypeRequest(data)
      .then((response: AddResourceTypeResponse) => {
        history.push("/edit-resource-type/" + response.id);
        toast.success("Zasób poprawnie dodany");
        toast.success("Pomyślnie zapisano zmiany");
      })
      .catch(() => {});
  };

  return (
    <div>
      <div className="pageHeader">
        <Container>
          <Row>
            <Col className="col-3 mt-3">
              <Link
                className="customLink"
                to="/admin-panel/resourceTypesManagment"
              >
                <FontAwesomeIcon
                  className="mr-2"
                  size="lg"
                  icon={faLongArrowAltLeft}
                ></FontAwesomeIcon>
                Lista typów zasobów
              </Link>
            </Col>
            <Col className="text-center mt-2 col-6">
              <h2>
                Stwórz nowy typ zasobu
              </h2>
            </Col>
            <Col className="col-3 mt-2">
              <Button
                onClick={handleSubmit(onSubmit)}
                disabled={!formState.isDirty}
                variant="success"
                className="float-right"
              >
                Stwórz nowy typ zasobu
              </Button>
            </Col>
          </Row>
        </Container>
      </div>
      <div className="pageContent mt-4">
        <Container>
          <Row className="mt-3">
            <Col>
              <Form>
                <Form.Group>
                  <Form.Label>Nazwa</Form.Label>
                  <Form.Control
                    id="name"
                    name="name"
                    ref={register}
                    className="bg-dark text-light"
                    type="text"
                    defaultValue={resourceTypeDetails?.name}
                  />
                </Form.Group>
                <Form.Group>
                  <Form.Label>Opis</Form.Label>
                  <Form.Control
                    id="description"
                    name="description"
                    ref={register}
                    className="bg-dark text-light"
                    as="textarea"
                    defaultValue={resourceTypeDetails?.description}
                    rows={4}
                  />
                </Form.Group>
              </Form>
            </Col>
            <Col>
              <Row>
                <Col>
                  <h3>Lista atrybutów:</h3>
                </Col>
                <Col className="col-4 float-right">
                  <Button variant="success" onClick={handleShow}>
                    Dodaj atrybut
                  </Button>
                </Col>
              </Row>
              <Table className="mt-2" striped bordered hover variant="dark">
                <thead>
                  <tr>
                    <th>Nazwa</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {resourceTypeDetails?.resourceTypeAttributes.map(
                    (element: ResourceTypeAttributeResponse, index: number) => (
                      <tr>
                        <td>{element.name}</td>
                        <td width="100px">
                          <FontAwesomeIcon
                            onClick={() => {
                              remove(element.attributeId);
                            }}
                            className="ml-3 trashClickableIcon"
                            size="lg"
                            icon={faTrashAlt}
                          />
                        </td>
                      </tr>
                    )
                  )}
                </tbody>
              </Table>
            </Col>
          </Row>
        </Container>
      </div>
      <AddAttributeModal
        show={show}
        existingAttributes={resourceTypeDetails?.resourceTypeAttributes}
        handleChoose={handleChoose}
        handleClose={handleClose}
      />
    </div>
  );
}

export default AddResourceType;
