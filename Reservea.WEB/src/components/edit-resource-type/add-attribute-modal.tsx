import React, { useEffect, useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { attributesListRequest } from "../../api/clients/resourcesClient";
import { AttributeForListResponse } from "../../api/dtos/resources/attributes/attributeForListResponse";
import { ResourceTypeAttributeResponse } from "../../api/dtos/resources/resourceTypeAttributes/resourceTypeAttributeResponse";

function AddAttributeModal({
  handleClose,
  handleChoose,
  show,
  existingAttributes,
}: any) {
  const [attributesList, setAttributesList] = useState<
    Array<AttributeForListResponse>
  >();

  const [selectedElement, setSelectedElement] = useState<number>();

  useEffect(() => {
    if (show) {
      attributesListRequest()
        .then((attributesList: Array<AttributeForListResponse>) => {
          setAttributesList(attributesList);
          setSelectedElement(
            attributesList?.find(
              (x) =>
                !existingAttributes.some(
                  (y: ResourceTypeAttributeResponse) => y.attributeId == x.id
                )
            )?.id
          );
        })
        .catch(() => {});
    }
  }, [show]);
  return (
    <Modal className="my-modal" show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Wybierz atrybut</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form.Control
          id="resourceTypeId"
          value={selectedElement}
          onChange={(e) => {
            setSelectedElement(Number(e.target.value));
          }}
          name="resourceTypeId"
          className="bg-dark text-light"
          as="select"
        >
          {attributesList?.map((element: AttributeForListResponse) => (
            <option
              disabled={existingAttributes.some(
                (x: ResourceTypeAttributeResponse) =>
                  x.attributeId == element.id
              )}
              value={element.id}
            >
              {element.name}
            </option>
          ))}
        </Form.Control>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Anuluj
        </Button>
        <Button
          variant="success"
          disabled={selectedElement === undefined}
          onClick={() => {
            handleChoose(attributesList?.find((x) => x.id == selectedElement));
          }}
        >
          Dodaj
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddAttributeModal;
