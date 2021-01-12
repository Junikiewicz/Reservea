import React, { useState } from "react";
import { Button, Col, Form, Modal, Row } from "react-bootstrap";

function AddUserRateModal({ handleClose, handleChoose, show }: any) {
  const [selectedElement, setSelectedElement] = useState<any>({});

  return (
    <Modal className="my-modal" show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Podziel się z nami swoją opinią</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row className="justify-content-center">
          <Col className="col-11">
            <Form.Control
              id="resourceTypeId"
              as="textarea"
              rows={4}
              onChange={(e) => {
                let element = { ...selectedElement };
                element.feedback = e.target.value;
                setSelectedElement(element);
              }}
              value={selectedElement.feedback}
              name="resourceTypeId"
              className="bg-dark text-light"
            />
          </Col>
        </Row>
        <Row className="mt-3">
          <Col style={{ fontSize: "14px" }} className="col-10 mt-2">
            Czy wyrażasz zgodę na publikację tej opinii na naszej stronie?
          </Col>
          <Col>
            <Form.Control
              name={`isChecked`}
              className="bg-dark text-light"
              type="checkbox"
              onChange={(e: any) => {
                let element = { ...selectedElement };
                element.isAllowedToBeShared = e.target.checked === true;
                setSelectedElement(element);
              }}
            />
          </Col>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Anuluj
        </Button>
        <Button
          variant="success"
          disabled={selectedElement?.length === 0}
          onClick={() => {
            handleChoose(selectedElement);
          }}
        >
          Dodaj
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default AddUserRateModal;
