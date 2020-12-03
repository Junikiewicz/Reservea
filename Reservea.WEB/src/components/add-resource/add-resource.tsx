import React, { useEffect, useState } from "react";
import { Button, Row, Tab, Nav, Col, Container } from "react-bootstrap";
import EditResourceGeneral from "../edit-resource/edit-resource-general/edit-resource-general";
import EditResourceAttributes from "../edit-resource/edit-resource-attributes/edit-resource-attributes";
import { ResourceDetailsResponse } from "../../api/dtos/resources/resources/resourceDetailsResponse";
import {
  createResourceRequest,
  resourcesTypeAttributesRequest,
  resourcesTypesListRequest,
} from "../../api/clients/resourcesClient";
import { useFieldArray, useForm } from "react-hook-form";
import { UpdateResourceFormData } from "../../common/models/forms/updateResourceFormData";
import { toast } from "react-toastify";
import { ResourceTypeForListResponse } from "../../api/dtos/resources/resourceTypes/resourceTypeForListResponse";
import { ResourceAttributeResponse } from "../../api/dtos/resources/resourceAttributes/resourceAttributeResponse";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLongArrowAltLeft } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import { AttributeForListResponse } from "../../api/dtos/resources/attributes/attributeForListResponse";
import { useHistory } from "react-router-dom";
import { AddResourceResponse } from "../../api/dtos/resources/resources/addResourceResponse";

function AddResource() {
  const history = useHistory();
  const [
    resourceDetails,
    setResourceDetails,
  ] = useState<ResourceDetailsResponse>();
  const [resourceTypes, setResourceTypes] = useState<
    Array<ResourceTypeForListResponse>
  >();

  const {
    control,
    register,
    handleSubmit,
    reset,
    getValues,
    formState,
  } = useForm<UpdateResourceFormData>({
    mode: "onSubmit",
  });

  const { fields, remove, insert } = useFieldArray<
    AttributeForListResponse,
    "customId"
  >({
    control,
    name: "resourceAttributes",
    keyName: "customId",
  });

  useEffect(() => {
    resourcesTypesListRequest()
      .then((resourceTypesResponse: Array<ResourceTypeForListResponse>) => {
        setResourceTypes(resourceTypesResponse);
        setResourceDetails({
          id: 0,
          name: "",
          description: "",
          pricePerHour: 0,
          resourceStatusId: 1,
          resourceTypeId: resourceTypes ? resourceTypes[0].id : 0,
          resourceAttributes: [],
        });
        onResourceTypeIdChange();
      })
      .catch(() => {});
  }, []);

  const onResourceTypeIdChange = async () => {
    resourcesTypeAttributesRequest(getValues("resourceTypeId"))
      .then((data: Array<AttributeForListResponse>) => {
        remove();
        insert(0, data);
    })
      .catch(() => {});
  };

  const onSubmit = async (data: UpdateResourceFormData): Promise<void> => {
    if (data.resourceAttributes) {
      data.resourceAttributes.map(
        (element: ResourceAttributeResponse, index: number) => {
          element.attributeId = fields[index].id ?? 0;
        }
      );
      createResourceRequest(data)
        .then((response: AddResourceResponse) => {
          history.push("/edit-resource/" + response.id);
          toast.success("Zasób poprawnie dodany");
        })
        .catch(() => {});
    }
  };

  return (
    <div>
      <Tab.Container defaultActiveKey="general">
        <div className="pageHeader">
          <Container>
            <Row>
              <Col className="col-4 mt-3">
                <Link
                  className="customLink"
                  to="/admin-panel/resourceManagment"
                >
                  <FontAwesomeIcon
                    className="mr-2"
                    size="lg"
                    icon={faLongArrowAltLeft}
                  ></FontAwesomeIcon>
                  Lista zasobów
                </Link>
              </Col>
              <Col className="text-center mt-2 col-4">
                <h2>Dodaj nowy zasób</h2>
              </Col>
              <Col className="col-4 mt-2">
                <Button
                  disabled={!formState.isDirty}
                  onClick={handleSubmit(onSubmit)}
                  variant="success"
                  className="float-right"
                >
                  Dodaj zasób
                </Button>
              </Col>
            </Row>
            <Row className="justify-content-center">
              <Nav variant="pills" className="customPillsColors">
                <Nav.Item>
                  <Nav.Link eventKey="general">Ogólne</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link eventKey="attributes">Atrybuty</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link eventKey="avaiability">Dostępność</Nav.Link>
                </Nav.Item>
              </Nav>
            </Row>
          </Container>
        </div>
        <div className="pageContent mt-4">
          <Container>
            <Tab.Content className="mt-4">
              <Tab.Pane eventKey="general">
                <EditResourceGeneral
                  resourceDetails={resourceDetails}
                  resourceTypes={resourceTypes}
                  register={register}
                  onResourceTypeIdChange={onResourceTypeIdChange}
                />
              </Tab.Pane>
              <Tab.Pane eventKey="attributes">
                <EditResourceAttributes
                  resourceAttributes={fields}
                  register={register}
                />
              </Tab.Pane>
              <Tab.Pane eventKey="avaiability"></Tab.Pane>
            </Tab.Content>
          </Container>
        </div>
      </Tab.Container>
    </div>
  );
}

export default AddResource;
