import React, { useEffect, useState } from "react";
import { Button, Row, Tab, Nav, Col, Container } from "react-bootstrap";
import EditResourceGeneral from "./edit-resource-general/edit-resource-general";
import EditResourceAttributes from "./edit-resource-attributes/edit-resource-attributes";
import { ResourceDetailsResponse } from "../../api/dtos/resources/resources/resourceDetailsResponse";
import {
  resourceAttributesForTypeChangeRequest,
  resourceDetailsRequest,
  resourcesTypesListRequest,
  updateResourceRequest,
} from "../../api/clients/resourcesClient";
import { useFieldArray, useForm } from "react-hook-form";
import { UpdateResourceFormData } from "../../common/models/forms/updateResourceFormData";
import { toast } from "react-toastify";
import { ResourceTypeForListResponse } from "../../api/dtos/resources/resourceTypes/resourceTypeForListResponse";
import { ResourceAttributeResponse } from "../../api/dtos/resources/resourceAttributes/resourceAttributeResponse";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLongArrowAltLeft } from "@fortawesome/free-solid-svg-icons";
import { Link } from "react-router-dom";
import { ResourceStatus } from "../../common/enums/resourceStatus";

function EditResource(props: any) {
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
    ResourceAttributeResponse,
    "customId"
  >({
    control,
    name: "resourceAttributes",
    keyName: "customId",
  });

  useEffect(() => {
    //TODO: REMOVE CHAIN
    resourceDetailsRequest(props.match.params.id)
      .then((resourceDetailsResponse: ResourceDetailsResponse) => {
        resourcesTypesListRequest()
          .then((resourceTypesResponse: Array<ResourceTypeForListResponse>) => {
            setResourceTypes(resourceTypesResponse);
            reset({
              resourceAttributes: resourceDetailsResponse.resourceAttributes,
            });
            setResourceDetails(resourceDetailsResponse);
          })
          .catch(() => {});
      })
      .catch(() => {});
  }, []);

  const onResourceTypeIdChange = async () => {
    resourceAttributesForTypeChangeRequest(
      props.match.params.id,
      getValues("resourceTypeId")
    )
      .then((data: Array<ResourceAttributeResponse>) => {
        remove();
        insert(0, data);
      })
      .catch(() => {});
  };

  const onSubmit = async (data: UpdateResourceFormData): Promise<void> => {
    console.log(data);
    if (data.resourceAttributes) {
      data.resourceAttributes.map(
        (element: ResourceAttributeResponse, index: number) => {
          element.attributeId = fields[index].attributeId ?? 0;
          element.name = fields[index].name ?? "";
        }
      );
    }
    updateResourceRequest(props.match.params.id, data)
      .then(() => {
        reset(data);
        toast.success("Pomyślnie zapisano zmiany");
      })
      .catch(() => {});
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
                <h2>
                  Zasób ID: <strong>{props.match.params.id}</strong>
                </h2>
              </Col>
              <Col className="col-4 mt-2">
                {resourceDetails?.resourceStatusId ===
                ResourceStatus.Removed ? (
                  <span className="float-right" style={{ color: "red" }}>Ten zasób został usunięty!</span>
                ) : (
                  <Button
                    disabled={
                      !formState.isDirty ||
                      resourceDetails?.resourceStatusId ==
                        ResourceStatus.Removed
                    }
                    onClick={handleSubmit(onSubmit)}
                    variant="success"
                    className="float-right"
                  >
                    Zapisz zmiany
                  </Button>
                )}
              </Col>
            </Row>
            <Row className="justify-content-center">
              <Nav variant="pills" className="customPillsColors">
                <Nav.Item>
                  <Nav.Link eventKey="general">Ogólne</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link
                    disabled={
                      resourceDetails?.resourceStatusId ==
                      ResourceStatus.Removed
                    }
                    eventKey="attributes"
                  >
                    Atrybuty
                  </Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link
                    disabled={
                      resourceDetails?.resourceStatusId ==
                      ResourceStatus.Removed
                    }
                    eventKey="avaiability"
                  >
                    Dostępność
                  </Nav.Link>
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

export default EditResource;
